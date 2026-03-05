using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10.Model;

namespace RestWithASPNET10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetingController : ControllerBase
    {
        private static long _id = 0;
        private static readonly string _template = "Hello, {0}!";


        [HttpGet]
        public Greeting Get([FromQuery] string name = "World")
        {
            long id = Interlocked.Increment(ref _id);
            string content = string.Format(_template, name);

            return new Greeting(id, content);
        }
    }
}
