using System.Threading.Tasks;
using Actio.Common.Auth;
using Actio.Common.Commands;
using Actio.Services.Identity.Services;
using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Identity.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        public AccountController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthenticateUser command)
            => Json(await _userService.LoginAsync(command.Email, command.Password));

        [HttpGet]
        public IActionResult Get() => Content("Test Area");
    }
}