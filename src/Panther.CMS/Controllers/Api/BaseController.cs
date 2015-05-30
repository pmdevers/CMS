using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;

namespace Panther.CMS.Controllers.Api
{
    [Authorize(Roles = "ApiUser")]
    [Route("api/[controller]")]
    public class BaseController : Controller
    {
         
    }
}