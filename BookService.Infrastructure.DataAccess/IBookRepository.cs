using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookService.Business.Models;

namespace BookService.Infrastructure.DataAccess
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllAsync();
        Task<Book> GetByIdAsync(int id);

        Task DeleteBookByIdAsync(int id);
        Task<Book> AddBookAsync(Book book);
        Task UpdateBook(Book book);

        Task SaveChangesAsync();
    }
}
