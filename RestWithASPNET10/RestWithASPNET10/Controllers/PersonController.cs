using Core;
using Microsoft.AspNetCore.Mvc;

namespace RestWithASPNET10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        private readonly ILogger<PersonController> _logger;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Retrieving all people");
            return Ok(_personService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Retrieving person with id {Id}", id);
            var person = _personService.FindById(id);
            if (person == null)
            {
                _logger.LogWarning("Person with id {Id} not found", id);
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Person person)
        {
            _logger.LogInformation("Creating a new person: {FirstName}", person.FirstName);
            var response = _personService.Create(person);
            if (response == null)
            {
                _logger.LogError("Failed to create person: {FirstName}", person.FirstName);
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Person person)
        {
            _logger.LogInformation("Updating person with id {Id}", person.Id);
            var response = _personService.Update(person);
            if (response == null)
            {
                _logger.LogWarning("Person with id {Id} not found for update", person.Id);
                return NotFound();
            }

            _logger.LogDebug("Person with id {Id} updated successfully", person.Id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Deleting person with id {Id}", id);
            _personService.Delete(id);
            _logger.LogDebug("Person with id {Id} deleted successfully", id);
            return NoContent();
        }
    }
}