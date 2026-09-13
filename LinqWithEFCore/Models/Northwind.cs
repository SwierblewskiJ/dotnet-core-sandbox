using Microsoft.EntityFrameworkCore;

namespace Models;

public class Northwind : DbContext
{
    public DbSet<Category> Categories{ get; set; } = null!;
    public DbSet<Product> Products { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string database = "Northwind.db";
        string folder = Environment.CurrentDirectory;
        string path = Path.Combine(folder, database);

        optionsBuilder.UseSqlite($"Data Source={path}");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        if(Database.ProviderName is not null && Database.ProviderName.Contains("Sqlite"))
        {
            modelBuilder.Entity<Product>().Property(p=>p.UnitPrice).HasConversion<double>();
        }
    }
}