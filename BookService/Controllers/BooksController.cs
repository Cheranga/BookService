using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using BookService.DTO;
using BookService.Models;

namespace BookService.Controllers
{
    /// <summary>
    /// Endpoint for Books
    /// </summary>
    public class BooksController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        /// <summary>
        /// HTTP GET request to retrieve all the books
        /// </summary>
        /// <returns>All the books</returns>
        public IQueryable<BookDto> GetBooks()
        {
            return db.Books.Select(x=> new BookDto
            {
                Id = x.Id,
                AuthorName = x.Author.Name,
                Title = x.Title
            });
        }

        // GET: api/Books/5
        [ResponseType(typeof(BookDetailDto))]
        public async Task<IHttpActionResult> GetBook(int id)
        {
            var bookDetail = await db.Books.Include(x=>x.Author)
                .Select(x=>new BookDetailDto
                {
                    Id = x.Id,
                    AuthorName = x.Author.Name,
                    Title = x.Title,
                    Genre = x.Genre,
                    Price = x.Price,
                    Year = x.Year
                })
                .SingleOrDefaultAsync(x=>x.Id == id);

            if (bookDetail == null)
            {
                return NotFound();
            }

            return Ok(bookDetail);
        }

        // PUT: api/Books/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutBook(int id, Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != book.Id)
            {
                return BadRequest();
            }

            db.Entry(book).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Books
        [ResponseType(typeof(BookDto))]
        public async Task<IHttpActionResult> PostBook(Book book)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Books.Add(book);
            await db.SaveChangesAsync();

            db.Entry(book).Reference(x=>x.Author).Load();

            var bookDto = new BookDto
            {
                Id = book.Id,
                AuthorName = book.Author.Name,
                Title = book.Title
            };

            return CreatedAtRoute("DefaultApi", new {id = bookDto.Id}, bookDto);
        }

        // DELETE: api/Books/5
        [ResponseType(typeof(Book))]
        public async Task<IHttpActionResult> DeleteBook(int id)
        {
            Book book = await db.Books.FindAsync(id);
            if (book == null)
            {
                return NotFound();
            }

            db.Books.Remove(book);
            await db.SaveChangesAsync();

            return Ok(book);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookExists(int id)
        {
            return db.Books.Count(e => e.Id == id) > 0;
        }
    }
}