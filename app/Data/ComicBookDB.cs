using Microsoft.EntityFrameworkCore;

namespace app.Data
{
    public class ComicBookDB : DbContext
    {
        public ComicBookDB(DbContextOptions<ComicBookDB> options)
            : base(options)
        {
        }

        public DbSet<ComicBook> ComicBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure your entities and relationships here
            base.OnModelCreating(modelBuilder);
        }
    }
}

