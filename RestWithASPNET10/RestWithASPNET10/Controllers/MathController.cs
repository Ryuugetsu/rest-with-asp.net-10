using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace RestWithASPNET10.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MathController : ControllerBase
    {
        private readonly MathService _service;

        public MathController(MathService service)
        {
            _service = service;
        }

        [HttpGet("{metod}/{firstNumber}/{secondNumber?}")]
        public IActionResult Get(string metod, string firstNumber, string? secondNumber) 
        {
            if (!NumberHelper.IsNumeric(firstNumber) || (secondNumber != null && !NumberHelper.IsNumeric(secondNumber)))
            {
                return BadRequest("Invalid Input!");
            }

            if (metod.ToLower() != "sqr" && secondNumber == null)
            {
                return BadRequest("Second number is required.");
            }

            decimal first = NumberHelper.ConvertToDecimal(firstNumber);
            decimal second = NumberHelper.ConvertToDecimal(secondNumber ?? "0");

            switch (metod.ToLower())
            {
                case "sum":
                    return Ok(_service.Sum(first, second));
                case "sub":
                    return Ok(_service.Sub(first, second));
                case "mult":
                    return Ok(_service.Mult(first, second));
                case "div":
                    return Ok(_service.Div(first, second));
                case "mean":
                    return Ok(_service.Mean(first, second));
                case "sqr":
                    return Ok(_service.Sqrt((double)first));
                default:
                    return BadRequest("Invalid Method!");
            }
        }
    }
}
