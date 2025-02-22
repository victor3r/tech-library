using library.api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace library.api.Infraestructure
{
    public class LibraryDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\www\\TechLibraryDb.db");
        }
    }
}
