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
        private readonly BookServiceContext db;

        /// <summary>
        /// This will create the books controller version 2
        /// </summary>
        /// <param name="context">The book service context, this will be injected</param>
        public BooksV2Controller(BookServiceContext context)
        {
            db = context;
        }

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