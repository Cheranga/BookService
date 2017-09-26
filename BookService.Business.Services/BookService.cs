using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService.Business.Models;
using BookService.Business.Services.DTO;
using BookService.Infrastructure.DataAccess;
using BookService.Infrastructure.DataAccess.EntityFramework;

namespace BookService.Business.Services
{
    public class BookService
    {
        private readonly IBookRepository _bookRepository;

        public BookService(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        public async Task<IEnumerable<BookDto>> GetAllAsync()
        {
            var books = await _bookRepository.GetAllAsync();
            var bookDtos = books.Select(x => new BookDto
            {
                Id = x.Id,
                AuthorName = x.Author.Name,
                Title = x.Title
            });

            return bookDtos;
        }

        public async Task<BookDetailDto> GetBookByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);
            var dto = new BookDetailDto
            {
                Id = book.Id,
                AuthorName = book.Author.Name,
                Title = book.Title,
                Genre = book.Genre,
                Price = book.Price,
                Year = book.Year
            };

            return dto;
        }

        public async Task<BookDto> AddBookAsync(Book book)
        {
            var insertedBook =  await _bookRepository.AddBookAsync(book);
            return new BookDto
            {
                Id = insertedBook.Id,
                AuthorName = insertedBook.Author.Name,
                Title = insertedBook.Title
            };
        }

        public async Task UpdateBook(Book book)
        {
            await _bookRepository.UpdateBook(book);
        }

        public async Task DeleteBookById(int id)
        {
            await _bookRepository.DeleteBookByIdAsync(id);
        }
    }
}
