using Microsoft.AspNetCore.Mvc;

namespace Actio.Services.Activities.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        [HttpGet]
        public IActionResult Get() => Content($"Hello from {nameof(HomeController)} - Activity Service");

        
    }
}