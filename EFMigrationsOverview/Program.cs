using Microsoft.EntityFrameworkCore;
using EFMigrationsOverview;

await using BlogContext context = new();
await context.Database.MigrateAsync();

if (!await context.Blogs.AnyAsync())
{
    context.Blogs.Add(new Blog { Name = "My first blog" });
    await context.SaveChangesAsync();
}

foreach (var blog in await context.Blogs.AsNoTracking().ToListAsync())
{
    Console.WriteLine($"{blog.Id}: {blog.Name}");
}