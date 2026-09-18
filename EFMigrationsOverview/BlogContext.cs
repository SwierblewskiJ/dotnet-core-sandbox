using Microsoft.EntityFrameworkCore;

namespace EFMigrationsOverview;

public class BlogContext : DbContext
{
    public DbSet<Blog> Blogs => Set<Blog>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string databasePath = Path.Combine(Environment.CurrentDirectory, "blog.db");
        optionsBuilder.UseSqlite($"Data Source={databasePath}");
    }
}