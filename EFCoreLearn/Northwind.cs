using Microsoft.EntityFrameworkCore;
using Models;

namespace EFCoreLearn.Data;

public class Northwind : DbContext
{
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }


    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        const string dbFile= "Northwind.db";
        string dbPath = Path.Combine(Environment.CurrentDirectory, dbFile);

        string text = $"Data Source={dbPath}";
        Console.WriteLine(text);
        optionsBuilder.UseSqlite(text);
    }

    protected override void OnModelCreating(
        ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>().Property(category => category.CategoryName)
        .IsRequired().HasMaxLength(15);

        if (Database.ProviderName?.Contains("SQLite") ?? false)
        {
            modelBuilder.Entity<Product>().Property(product=> product.Price).HasConversion<double>();
        }
    }


}