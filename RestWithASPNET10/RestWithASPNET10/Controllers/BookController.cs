using Core;
using Microsoft.AspNetCore.Mvc;

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
        public IActionResult Get()
        {
            _logger.LogInformation("Retrieving all people");
            return Ok(_bookService.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            _logger.LogInformation("Retrieving book with id {Id}", id);
            var book = _bookService.FindById(id);
            if (book == null)
            {
                _logger.LogWarning("Book with id {Id} not found", id);
                return NotFound();
            }

            return Ok(book);
        }

        [HttpPost]
        public IActionResult Post([FromBody] Book book)
        {
            _logger.LogInformation("Creating a new book: {Title}", book.Title);
            var response = _bookService.Create(book);
            if (response == null)
            {
                _logger.LogError("Failed to create book: {Title}", book.Title);
                return NotFound();
            }

            return Ok(response);
        }


        [HttpPut]
        public IActionResult Put([FromBody] Book book)
        {
            _logger.LogInformation("Updating book with id {Id}", book.Id);
            var response = _bookService.Update(book);
            if (response == null)
            {
                _logger.LogWarning("Book with id {Id} not found for update", book.Id);
                return NotFound();
            }

            _logger.LogDebug("Book with id {Id} updated successfully", book.Id);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Deleting book with id {Id}", id);
            _bookService.Delete(id);
            _logger.LogDebug("Book with id {Id} deleted successfully", id);
            return NoContent();
        }
    }
}