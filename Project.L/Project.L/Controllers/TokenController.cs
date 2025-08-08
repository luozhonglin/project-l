using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Entities;
using Project.Entities.UserToken;
using Project.SDK.Redis;
using Project.Service;
using StackExchange.Redis;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Mysqlx.Expect.Open.Types.Condition.Types;

namespace Project.L.Controllers
{
    public class TokenController : BaseApiController
    {

        public IUserInfoService UserInfoService { get; set; }
        private readonly IConfiguration _config;
        private readonly RedisHelper _redisHelper;
        public TokenController(IConfiguration config, IConnectionMultiplexer redis)
        {
            _config = config;
            _redisHelper = new RedisHelper(redis, "Token");
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] TokenRequest loginModel)
        {
            var user = await UserInfoService.GetInfoAsync(loginModel.AccountName, loginModel.Password);
            if (user == null) return Unauthorized();

            var token = GenerateJwtToken(user);
            await _redisHelper.SetStringAsync("refreshId", Guid.NewGuid().ToString(), TimeSpan.FromMinutes(10));
            return Ok(new { token });
        }

        private string GenerateJwtToken(UserInfo user)
        {
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.username),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Name, user.username),
            new Claim("UserId",user.id.ToString())
            };
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
