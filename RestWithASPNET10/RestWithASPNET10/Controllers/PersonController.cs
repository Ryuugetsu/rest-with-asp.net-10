using Core;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10.Data;
using RestWithASPNET10.Data.Converter;

namespace RestWithASPNET10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[EnableCors("LocalPolicy")]
    public class PersonController : ControllerBase
    {
        private IPersonService _personService;
        private readonly ILogger<PersonController> _logger;
        private readonly PersonConverter _converter;

        public PersonController(IPersonService personService, ILogger<PersonController> logger)
        {
            _personService = personService;
            _logger = logger;
            _converter = new PersonConverter();
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<PersonDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            _logger.LogInformation("Retrieving all people");
            return Ok(_converter.ParseList(_personService.FindAll()));
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Retrieving person with id {Id}", id);

            PersonDTO person = _converter.Parse(_personService.FindById(id));
            if (person == null)
            {
                _logger.LogWarning("Person with id {Id} not found", id);
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Creating a new person: {FirstName}", person.FirstName);

            var request = _converter.Parse(person);
            var response = _converter.Parse(_personService.Create(request));
            if (response == null)
            {
                _logger.LogError("Failed to create person: {FirstName}", person.FirstName);
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] PersonDTO person)
        {
            _logger.LogInformation("Updating person with id {Id}", person.Id);

            Person request = _converter.Parse(person);
            PersonDTO response = _converter.Parse(_personService.Update(request));
            if (response == null)
            {
                _logger.LogWarning("Person with id {Id} not found for update", person.Id);
                return NotFound();
            }

            _logger.LogDebug("Person with id {Id} updated successfully", person.Id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Deleting person with id {Id}", id);

            _personService.Delete(id);

            _logger.LogDebug("Person with id {Id} deleted successfully", id);
            return NoContent();
        }

        [HttpPatch("{id}")]
        [ProducesResponseType(200, Type = typeof(PersonDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Patch(int id)
        {
            _logger.LogInformation("Disabling person with id {Id}", id);

            PersonDTO response = _converter.Parse(_personService.Disable(id));
            if (response == null)
            {
                _logger.LogWarning("Person with id {Id} not found for disable", id);
                return NotFound();
            }

            _logger.LogDebug("Person with id {Id} disabled successfully", id);
            return Ok(response);
        }
    }
}