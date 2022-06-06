using Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private IApplicationUser _user;

        public UserController(IApplicationUser user)
        {
            _user = user;
        }

        public IActionResult Get()
        {
            return Ok(_user);
        }
    }
}
