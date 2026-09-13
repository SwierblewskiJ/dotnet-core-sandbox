using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Northwind.Models;


partial class Program
{
    private static void GetProducts(int[]? idMainProducts= null)
    {
        using var db = new EFCoreLearn.Data.Northwind();

        var products = db.Products;

        if(products is null || !products.Any())
        {
            Error("No products were found"); return;
        }

        Console.WriteLine("| {0,-3} | {1,-35} | {2,8} | {3,5} | {4} |","Id","Product Name","Price","Quantity","Uncontinued");

        foreach(var product in db.Products)
        {
            ConsoleColor previousColor = Console.ForegroundColor;

            if((idMainProducts is not null) && idMainProducts.Contains(product.ProductId))
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }

            Console.WriteLine("| {0:000} | {1,-35} | {2,8:$#,##0.00} | {3,5} | {4} |",product.ProductId,product.ProductName,product.Price,product.InStock,product.Discontinued);
        
            Console.ForegroundColor = previousColor;
        }
    }

    private static (int changed, int productId) AddProduct(
        int categoryId, string productName, decimal? price, short? inStock)
    {
        using var db = new EFCoreLearn.Data.Northwind();

        Product p = new()
        {
            CategoryId = categoryId,
            ProductName = productName,
            Price = price,
            InStock = inStock
        };

        EntityEntry<Product> entity = db.Products.Add(p);
        Console.WriteLine($"State: {entity.State}, Product's ID: {p.ProductId}");

        int changed = db.SaveChanges();
        Console.WriteLine($"State: {entity.State}, Product's ID: {p.ProductId}");


        return (changed, p.ProductId);
    }

    private static (int changed, int productID) IncreseProductPrice(
        string namePrefix, decimal price)
    {
        using var db = new EFCoreLearn.Data.Northwind();

        if(db.Products is null) return (0,0);

        Product productToUpdate = db.Products.First(p=>p.ProductName.StartsWith(namePrefix));

        productToUpdate.Price += price;

        int changed = db.SaveChanges();

        return(changed, productToUpdate.ProductId);

    }

    private static int DeleteProduct(string namePrefix)
    {
        using var db = new EFCoreLearn.Data.Northwind();

        IQueryable<Product>? products = db.Products?.Where(p=>p.ProductName.StartsWith(namePrefix));

        if((products is null) || (!products.Any()))
        {
            Console.WriteLine("Not found product to delete"); return 0;
        }
        else
        {
            if (db.Products is null) return 0;

            db.Products.RemoveRange(products);
        }

        int changed = db.SaveChanges();
        return changed;
    }

    private static (int changed, int[]? productsID) QuickerIncreaseProductsPrice(string namePrefix, decimal price)
    {
        using var db = new EFCoreLearn.Data.Northwind();

        if(db.Products is null) return(0,null);

        IQueryable<Product>? products = db.Products.Where(p=>p.ProductName.StartsWith(namePrefix));

        int changed = products.ExecuteUpdate(c=>c.SetProperty(p=>p.Price, p=>p.Price+ price));

        int[] productsID = products.Select(p=>p.ProductId).ToArray();
        return (changed, productsID);
    }

    private static int QuickerDeleteProducts(string namePrefix)
    {
        using var db = new EFCoreLearn.Data.Northwind();

        int changed =0;

        IQueryable<Product>? products = db.Products.Where(p=>p.ProductName.StartsWith(namePrefix));

         if((products is null) || (!products.Any()))
        {
            Console.WriteLine("Not found product to delete"); return 0;
        } else
        {
            changed = products.ExecuteDelete();
        }
        return changed;
    }
    
}