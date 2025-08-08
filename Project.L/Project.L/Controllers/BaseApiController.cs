using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.L.Common;

namespace Project.L.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    [Authorize]
    [ServiceFilter(typeof(GlobalExceptionMiddleware))]
    public class BaseApiController : ControllerBase
    {

    }
}
