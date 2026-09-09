using Microsoft.EntityFrameworkCore;

namespace EFCoreLearn.Data;

public class Northwind : DbContext
{
    protected override void OnConfiguring(
        DbContextOptionsBuilder optionsBuilder)
    {
        const string dbFile= "Northwind.db";
        string dbPath = Path.Combine(Environment.CurrentDirectory, dbFile);

        string text = $"Data Source={dbPath}";
        Console.WriteLine(text);
        optionsBuilder.UseSqlite(text);
    }
}