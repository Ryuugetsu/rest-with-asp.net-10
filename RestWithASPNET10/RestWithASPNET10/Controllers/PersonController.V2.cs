using Core;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10.Data;
using RestWithASPNET10.Data.Converter;

namespace RestWithASPNET10.Controllers.v2
{
    [ApiController]
    [Route("api/[controller]/v2")]
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
        public IActionResult Get()
        {
            _logger.LogInformation("Retrieving all people");
            return Ok(_personService.FindAll().Adapt<List<PersonDTOV2>>());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Retrieving person with id {Id}", id);

            PersonDTOV2 person = _personService.FindById(id).Adapt<PersonDTOV2>();
            if (person == null)
            {
                _logger.LogWarning("Person with id {Id} not found", id);
                return NotFound();
            }

            return Ok(person);
        }

        [HttpPost]
        public IActionResult Post([FromBody] PersonDTOV2 person)
        {
            _logger.LogInformation("Creating a new person: {FirstName}", person.FirstName);

            Person request = person.Adapt<Person>() ;
            PersonDTOV2 response = _personService.Create(request).Adapt<PersonDTOV2>();
            if (response == null)
            {
                _logger.LogError("Failed to create person: {FirstName}", person.FirstName);
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPut]
        public IActionResult Put([FromBody] PersonDTOV2 person)
        {
            _logger.LogInformation("Updating person with id {Id}", person.Id);

            Person request = person.Adapt<Person>();
            PersonDTOV2 response = _personService.Update(request).Adapt<PersonDTOV2>();
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