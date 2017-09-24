using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using BookService.DTO;
using BookService.Models;

namespace BookService.Controllers
{
    /// <summary>
    /// The latest version of the books endpoint
    /// </summary>
    public class BooksV2Controller : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        /// <summary>
        /// HTTP GET request to retrieve all the books
        /// </summary>
        /// <returns>All the books</returns>
        public IQueryable<BookDtoV2> GetBooks()
        {
            var books = db.Books.Select(x => new BookDtoV2
            {
                Id = x.Id,
                AuthorName = x.Author.Name,
                Title = x.Title,
                Year = x.Year
            });

            return books;
        }
    }
}