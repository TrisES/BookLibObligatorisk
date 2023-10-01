using Microsoft.EntityFrameworkCore;
using BookLibObligatorisk.Models;

namespace BookLibObligatorisk
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
    }
}
