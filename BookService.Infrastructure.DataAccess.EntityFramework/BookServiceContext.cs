using System.Data.Entity;
using System.Diagnostics;
using BookService.Business.Models;

namespace BookService.Infrastructure.DataAccess.EntityFramework
{
    public class BookServiceContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx
    
        public BookServiceContext() : base("name=BookService")
        {
            Database.Log = s => Debug.WriteLine(s);
        }

        public System.Data.Entity.DbSet<Author> Authors { get; set; }

        public System.Data.Entity.DbSet<Book> Books { get; set; }
    }
}
