using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNET10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HomeController : Controller
    {
        [HttpPost("Login")]
        public IActionResult Login()
        {
            return Ok();
        }
    }
}
