using EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

public static class NorthwindContextExtensions
{
    public static IServiceCollection AddNorthwind(
        this IServiceCollection services,
        string path="..", string database = "Northwind.db"
    )
    {
        string fullPath = Path.Combine(path, database);
        fullPath = Path.GetFullPath(fullPath);
        NorthwindProtocol.WriteLine($"Database path: {fullPath}");

        if (!File.Exists(fullPath))
        {
            throw new FileNotFoundException(
            $"Not found {fullPath}.", fullPath);
        }

        services.AddDbContext<NorthwindContext>(options =>
        {
            options.UseSqlite($"Data Source={fullPath}");

            options.LogTo(NorthwindProtocol.WriteLine,new[] {Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting});

        }, contextLifetime: ServiceLifetime.Transient,
            optionsLifetime:ServiceLifetime.Transient);

        return services;
    }
}