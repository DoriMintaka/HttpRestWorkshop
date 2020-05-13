using Microsoft.EntityFrameworkCore;

namespace HttpRestWorkshop.DAL.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            Database.Migrate();
        }

        public DbSet<BoardGame> BoardGames { get; set; }
    }
}
