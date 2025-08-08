using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.L.Common;
using Project.SDK;
using System.Security.Claims;

namespace Project.L.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ServiceFilter(typeof(GlobalExceptionMiddleware))]
    public class BaseApiController : ControllerBase
    {
        #region 登录用户信息

        /// <summary>
        /// 当前用户ID
        /// </summary>
        public Guid CurrentUserId
        {
            get
            {
                var identity = User.Identity as ClaimsIdentity;
                var userId = SafeConvert.ToGuid(identity?.Claims.FirstOrDefault(c => c.Type == "UserId")?.Value??"");
                if (userId == Guid.Empty)
                {
                    throw new Exception("未登录");
                }

                return userId;
            }
        }
        #endregion
    }
}
