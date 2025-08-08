using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Project.Entities;
using Project.Entities.UserToken;
using Project.SDK;
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

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] TokenRequest loginModel)
        {
            var user = await UserInfoService.GetInfoAsync(loginModel.AccountName, loginModel.Password);
            if (user == null) return Unauthorized();

            var token = GenerateJwtToken(user);

            var refreshToken = Guid.NewGuid().ToString("n");
            await _redisHelper.SetStringAsync($"RefreshTokenId:{user.id.ToString()}", refreshToken, TimeSpan.FromMinutes(SafeConvert.ToInt32(_config["Jwt:RefreshTokenExpiration"])));
            var result = new TokenResponse()
            {
                AccessToken = token,
                RefreshToken = refreshToken,
                AccessTokenExpiration = DateTime.Now.AddMinutes(SafeConvert.ToInt32(_config["Jwt:AccessTokenExpiration"])),
                RefreshTokenExpiration = DateTime.Now.AddMinutes(SafeConvert.ToInt32(_config["Jwt:RefreshTokenExpiration"]))

            };
            return new JsonResult(result);
        }


        /// <summary>
        /// 刷新token
        /// </summary>
        /// <param name="loginModel"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] TokenRefreshRequest request)
        {
            UserInfo userInfo;
            TokenResponse result = new TokenResponse();
            try
            {
                JwtSecurityTokenHandler jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
                var oldToken = await HttpContext.GetTokenAsync("access_token");
                if (string.IsNullOrWhiteSpace(oldToken))
                {
                    return BadRequest(new
                    {
                        error = "登录已经失效，请重新登录"
                    });
                }

                var tokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = _config["Jwt:Issuer"],
                    ValidateAudience = true,
                    ValidAudience = _config["Jwt:Audience"],
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"])),
                    ValidateLifetime = true, // 验证过期时间
                    ClockSkew = TimeSpan.Zero // 可设置时钟偏移量
                };

                // 验证令牌并获取ClaimsPrincipal
                var principal = jwtSecurityTokenHandler.ValidateToken(oldToken, tokenValidationParameters, out var validatedToken);
                var userId = SafeConvert.ToGuid(principal.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value ?? "");
                userInfo = await UserInfoService.GetByIdAsync(userId);
                if (userInfo == null) return Unauthorized();

                var oldRefreshToken =await _redisHelper.GetStringAsync($"RefreshTokenId:{userInfo.id.ToString()}");
                if (string.IsNullOrWhiteSpace(oldRefreshToken))
                {
                    return BadRequest(new
                    {
                        error = "登录已经失效，请重新登录"
                    });
                }

                if (oldRefreshToken != request.RefreshToken)
                {
                    return BadRequest(new
                    {
                        error = "登录已经失效，请重新登录"
                    });
                }

                var token = GenerateJwtToken(userInfo);
                var refreshToken = Guid.NewGuid().ToString("n");
                await _redisHelper.SetStringAsync($"RefreshTokenId:{userInfo.id.ToString()}", refreshToken, TimeSpan.FromMinutes(SafeConvert.ToInt32(_config["Jwt:RefreshTokenExpiration"])));
                result = new TokenResponse()
                {
                    AccessToken = token,
                    RefreshToken = refreshToken,
                    AccessTokenExpiration = DateTime.Now.AddMinutes(SafeConvert.ToInt32(_config["Jwt:AccessTokenExpiration"])),
                    RefreshTokenExpiration = DateTime.Now.AddMinutes(SafeConvert.ToInt32(_config["Jwt:RefreshTokenExpiration"]))

                };

            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    error = "登录已经失效，请重新登录"
                });
            }

            return new JsonResult(result);
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
                expires: DateTime.Now.AddMinutes(SafeConvert.ToInt32(_config["Jwt:AccessTokenExpiration"])),//设置60分钟后过期
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
