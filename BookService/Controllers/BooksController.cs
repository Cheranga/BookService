using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookService.Business.Models;
using BookService.Business.Services.DTO;

namespace BookService.Controllers
{
    /// <summary>
    ///     Endpoint for Books
    /// </summary>
    public class BooksController : ApiController
    {
        private readonly Business.Services.BookService _bookService;

        /// <summary>
        ///     Creates a books controller
        /// </summary>
        /// <param name="bookService">The application service to handle books</param>
        public BooksController(Business.Services.BookService bookService)
        {
            _bookService = bookService;
        }

        /// <summary>
        ///     HTTP GET request to retrieve all the books
        /// </summary>
        /// <returns>All the books</returns>
        public async Task<IQueryable<BookDto>> GetBooksAsync()
        {
            var books = await _bookService.GetAllAsync();
            return books.AsQueryable();
        }

        // GET: api/Books/5
        /// <summary>
        ///     Gets a book by id
        /// </summary>
        /// <param name="id">The required book id</param>
        /// <returns>If exists returns the book by id, otherwise null</returns>
        [ResponseType(typeof (BookDetailDto))]
        public async Task<IHttpActionResult> GetBookAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }

        // PUT: api/Books/5
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="book"></param>
        /// <returns></returns>
        [ResponseType(typeof (void))]
        public async Task<IHttpActionResult> PutBookAsync(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            try
            {
                await _bookService.UpdateBook(book);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await BookExistsAsync(id))
                {
                    return NotFound();
                }

                throw;
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        /// <summary>
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        [ResponseType(typeof (BookDto))]
        public async Task<IHttpActionResult> PostBookAsync(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var bookDto = await _bookService.AddBookAsync(book);

            return CreatedAtRoute("DefaultApi", new {id = bookDto.Id}, bookDto);
        }

        // DELETE: api/Books/5
        /// <summary>
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> DeleteBookAsync(int id)
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            await _bookService.DeleteBookById(id);

            return Ok(book);
        }

        /// <summary>
        ///     Releases the unmanaged resources that are used by the object and, optionally, releases the managed resources.
        /// </summary>
        /// <param name="disposing">
        ///     true to release both managed and unmanaged resources; false to release only unmanaged
        ///     resources.
        /// </param>
        protected override void Dispose(bool disposing)
        {
            //if (disposing)
            //{
            //    db.Dispose();
            //}
            base.Dispose(disposing);
        }

        private async Task<bool> BookExistsAsync(int id)
        {
            return await _bookService.GetBookByIdAsync(id) != null;
        }
    }
}