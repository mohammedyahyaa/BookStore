using Microsoft.EntityFrameworkCore;

namespace BookStore.Models
{
    public class BookStoreDbContext :DbContext
    {

        public BookStoreDbContext(DbContextOptions<BookStoreDbContext> options):base(options)
        {
                
        }
        public DbSet<Book> Books { get; set; }
        public DbSet <Author> Authors { get; set; }

    }
}
