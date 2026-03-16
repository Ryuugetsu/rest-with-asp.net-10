using Core;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using RestWithASPNET10.Data;

namespace RestWithASPNET10.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private IBookService _bookService;
        private readonly ILogger<BookController> _logger;

        public BookController(IBookService bookService, ILogger<BookController> logger)
        {
            _bookService = bookService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(200, Type = typeof(List<BookDTO>))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get()
        {
            _logger.LogInformation("Retrieving all people");
            List<BookDTO> book = _bookService.FindAll().Adapt<List<BookDTO>>();
            return Ok(book);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Retrieving book with id {Id}", id);
            BookDTO book = _bookService.FindById(id).Adapt<BookDTO>();
            if (book == null)
            {
                _logger.LogWarning("Book with id {Id} not found", id);
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Post([FromBody] BookDTO book)
        {
            _logger.LogInformation("Creating a new book: {Title}", book.Title);

            Book request = book.Adapt<Book>();
            BookDTO response = _bookService.Create(request).Adapt<BookDTO>();
            if (response == null)
            {
                _logger.LogError("Failed to create book: {Title}", book.Title);
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPut]
        [ProducesResponseType(200, Type = typeof(BookDTO))]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Put([FromBody] BookDTO book)
        {
            _logger.LogInformation("Updating book with id {Id}", book.Id);

            Book request = book.Adapt<Book>();
            BookDTO response = _bookService.Update(request).Adapt<BookDTO>();
            if (response == null)
            {
                _logger.LogWarning("Book with id {Id} not found for update", book.Id);
                return NotFound();
            }

            _logger.LogDebug("Book with id {Id} updated successfully", book.Id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(statusCode:StatusCodes.Status204NoContent)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Deleting book with id {Id}", id);
            _bookService.Delete(id);
            _logger.LogDebug("Book with id {Id} deleted successfully", id);
            return NoContent();
        }
    }
}