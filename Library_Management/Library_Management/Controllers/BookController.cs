using Library_Management.DTO;
using Library_Management.Entities;
using Library_Management.Interface;
using Library_Management.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos;

namespace Library_Management.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        public readonly Container _container;
        public IBookInterface _bookInterface;

        private Container GetContainer()
        {
            string URI = Environment.GetEnvironmentVariable("URI");
            string PrimaryKey = Environment.GetEnvironmentVariable("PrimaryKey");
            string DatabaseName = Environment.GetEnvironmentVariable("DatabaseName");
            string ContainerName = Environment.GetEnvironmentVariable("ContainerNameL");
            CosmosClient cosmosclient = new CosmosClient(URI, PrimaryKey);
            //Connect with Our Database
            Database databse = cosmosclient.GetDatabase(DatabaseName);
            //Connect with Our Container 
            Container container = databse.GetContainer(ContainerName);

            return container;
        }
        public BookController(IBookInterface bookInterface)
        {
            _container = GetContainer();
            _bookInterface = bookInterface;
        }

        [HttpPost]
        public async Task<IActionResult> AddBook(BookDTO bookDTO)
        {
            BookEntity bookEntity = new BookEntity();
            // Convert book model to book entity

            bookEntity.Title = bookDTO.Title;
            bookEntity.Author = bookDTO.Author;
            bookEntity.Subject = bookDTO.Subject;
   
            // Call service function
            var responce = await _bookInterface.AddBook(bookEntity);
            // Return model to UI
            bookDTO.Title = responce.Title;
            bookDTO.Author = responce.Author;
            bookDTO.Subject = responce.Subject;

            return Ok(bookDTO);
        }

        [HttpGet]
        public async Task<IActionResult> ReadAllBooks()
        {
            try
            {
                var allBooks = await _bookInterface.ReadAllBooks();
                return Ok(allBooks);    
            }
            catch (Exception ex)
            {
                return BadRequest($"Error while getting books: {ex.Message}");
            }
        }

        [HttpGet("{bookId}")]
        public async Task<IActionResult> ReadBookById(string bookId)
        {
            try
            {
                var book = await _bookInterface.ReadBookById(bookId);

                if (book == null)
                {
                    return NotFound(); 
                }

                return Ok(book); 
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        [HttpPost]
        public async Task<IActionResult> UpdateBook(string bookId, BookEntity updatedBook)
        {
            // Calling service to update the book
            var result = await _bookInterface.UpdateBook(bookId, updatedBook);

            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound($"Book with ID {bookId} not found");
            }
        }

        [HttpDelete("{bookId}")]
        public async Task<IActionResult> DeleteBookByUid(string bookId)
        {
            var result = await _bookInterface.DeleteBookByUid(bookId);
            if (result)
            {
                return Ok("Deleted Successfully!");
            }
            else
            {
                return NotFound($"Book with ID {bookId} not found");
            }
        }

        [HttpPost]
        public async Task<IActionResult> IssueBook(string userId, string bookId)
        {
            var issuedBook = await _bookInterface.IssueBook(userId, bookId);

            if (issuedBook != null)
            {
                return Ok(issuedBook);
            }
            else
            {
                return BadRequest("Book could not be issued. Check availability.");
            }
        }
    }
}
