using Common.Constants;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    [Route(ApiRoutes.BaseRoute)]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public BaseController()
        {

        }
    }
}
