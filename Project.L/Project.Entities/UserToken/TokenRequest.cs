using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Entities.UserToken
{
    public class TokenRequest
    {
        /// <summary>
        /// 登录名
        /// </summary>
        public string AccountName { get; set; }

        /// <summary>
        /// 密码，当授权类型为password必传
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 授权类型 password / refresh_token
        /// </summary>
        public string GrantType { get; set; }

        /// <summary>
        /// 刷新Token，当授权类型为refresh_token必传
        /// </summary>
        public string RefreshToken { get; set; }

    }

    public class TokenResponse
    {
        /// <summary>
        /// 访问请求Token
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// 刷新Token，用于刷新授权Token
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// token过期时间
        /// </summary>
        public DateTime AccessTokenExpiration { get; set; }

        /// <summary>
        /// 刷新token过期时间
        /// </summary>
        public DateTime RefreshTokenExpiration { get; set; }

    }


    public class TokenRefreshRequest
    {
        /// <summary>
        /// 刷新Token，当授权类型为refresh_token必传
        /// </summary>
        public string RefreshToken { get; set; }

    }
}
