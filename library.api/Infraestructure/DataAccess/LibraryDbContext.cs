using library.api.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace library.api.Infraestructure.DataAccess;

public class LibraryDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }

    override protected void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var currentDirectory = Directory.GetCurrentDirectory();
        var parentDirectory = Directory.GetParent(currentDirectory)?.FullName;
        var dbPath = Path.Combine(parentDirectory, "TechLibraryDb.db");

        optionsBuilder.UseSqlite($"Data Source={dbPath}");
    }
}