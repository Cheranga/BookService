using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using BookService.Business.Models;

namespace BookService.Infrastructure.DataAccess.EntityFramework
{
    public class BookRepository : IBookRepository
    {
        private readonly BookServiceContext _context;

        public BookRepository(BookServiceContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Book>> GetAllAsync()
        {
            var books = await _context.Books.Include(x => x.Author).ToListAsync();
            return books;
        }

        public async Task<Book> GetByIdAsync(int id)
        {
            var book = await _context.Books.Include(x => x.Author).SingleOrDefaultAsync(x => x.Id == id);
            return book;
        }

        public async Task DeleteBookByIdAsync(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return;
            }

            _context.Books.Remove(book);
        }

        public async Task<Book> AddBookAsync(Book book)
        {
            _context.Books.Add(book);
            await SaveChangesAsync();

            await _context.Entry(book).Reference(x => x.Author).LoadAsync();
            return book;
        }

        public async Task UpdateBook(Book book)
        {
            _context.Entry(book).State = EntityState.Modified;
            await SaveChangesAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}