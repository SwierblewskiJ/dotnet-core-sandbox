using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Northwind.Models;

partial class Program
{
    private static void QueryCategories()
    {
        using var db = new EFCoreLearn.Data.Northwind();

        SectionTitle("Category list & their products:");

        IQueryable<Category> categories = db.Categories.Include(c => c.Products);

        if(categories is null || !categories.Any())
        {
            Error("Not found any categories");
            return;
        }

        foreach (var category in categories)
        {
            Console.WriteLine($"Category {category.CategoryName} has {category.Products.Count} products.");
        }

    }

    private static void CategoryQueryFilter()
    {
        using var db = new EFCoreLearn.Data.Northwind();

        SectionTitle("Products with minimal amount in stock");

        string? input;

        int howManyInStock;

        do
        {
            Console.WriteLine($"Write minimal amount in stock: ");
            input = Console.ReadLine();
        } while (!int.TryParse(input, out howManyInStock));

        IQueryable<Category>? categories = db.Categories?.Include(c => c.Products.Where(p => p.InStock >= howManyInStock));

        if(categories is null)
        {
            Error("Not found category"); return;
        }

        foreach(var c in categories)
        {
            Console.WriteLine($"Category {c.CategoryName} has {c.Products.Count} products with at least {howManyInStock} in stock.");

            foreach(var p in c.Products)
            {
                Console.WriteLine($" Product {p.ProductName}: {p.InStock} pieces");
            }
        }
        
    }

    private static void ProductQuery()
    {
        using var db = new EFCoreLearn.Data.Northwind();

        SectionTitle("Products that cost more than written; sorted descending");

        string? input;
        decimal price;

        do
        {
            Console.WriteLine("Provide product price: ");
            input = Console.ReadLine();
        } while(!decimal.TryParse(input, out price));

        IQueryable<Product>? products = db.Products?.Where(p => p.Price > price).OrderByDescending(p => p.Price);

        if((products is null) || (!products.Any()))
        {
            Error("Not found any "); return;
        }

        Info($"ToQueryString: {products.ToQueryString()}");

        foreach (var product in products)
        {
Console.WriteLine(
    $"{product.ProductId}: {product.ProductName} costs {product.Price:$#,##0.00}. " +
    $"{product.InStock} available in stock.");        }
    }

    private static void GetOneProduct()
    {
        using var db = new EFCoreLearn.Data.Northwind();

        SectionTitle("Get one product:");
        string? input;
        int id;

        do
        {
            Console.WriteLine("Provide product's id");
            input = Console.ReadLine();
        } while(!int.TryParse(input, out id));

        Product? product = db.Products?.First(p=>p.ProductId == id);


        Info($"First: {product?.ProductName}");

        if(product is null) Error($"Not found any product using First");

        product = db.Products?.Single(p=>p.ProductId == id);

        Info($"Single: {product?.ProductName}");


        if(product is null) Error("Not found any product using Single");
    }

    private static void QueryWithLike()
    {
        using var db = new EFCoreLearn.Data.Northwind();

        SectionTitle("Like output: ");

        Console.WriteLine("Provide part of product's name");
        string? input = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(input))
        {
            Error("Input not provided"); return;
        }

        IQueryable<Product>? products= db.Products?.Where(p=> EF.Functions.Like(p.ProductName, $"%{input}%"));

        if((products is null) || (!products.Any()))
        {
            Error("Not found any products"); return;
        }

        foreach(var product in products)
        {
            Console.WriteLine($"{product.ProductName}: in stock - {product.InStock}. Product is not being produced anymore ? {product.Discontinued}");
        }
    }

    private static void QueryRandomProduct()
    {
        using var db = new EFCoreLearn.Data.Northwind();

        SectionTitle("Get random product: ");

       int? rowsCount = db.Products?.Count();
       if(rowsCount is null)
        {
            Error("Table is empty"); return;
        }


       Product? product = db.Products?.FirstOrDefault(p => p.ProductId == (int)(EF.Functions.Random() * rowsCount));

       if(product == null)
        {
            Error("No product"); return;
        }

        Console.WriteLine($"Random product: {product.ProductId} {product.ProductName}");

       
    }



}