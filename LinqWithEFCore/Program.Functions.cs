using Models;
using Microsoft.EntityFrameworkCore;

partial class Program
{
    private static void FilterAndSort()
    {
        SectionTitle("FilterAndSort");

        using Northwind db = new();

        DbSet<Product> allProducts = db.Products;

        IQueryable<Product> filtredProducts = allProducts.Where(p=>p.UnitPrice<10M);

        IOrderedQueryable<Product> sortedProducts = filtredProducts.OrderByDescending(p=>p.UnitPrice);

        var selectionProducts = sortedProducts.Select(product => new
        {
            product.ProductId,
            product.ProductName,
            product.UnitPrice,
        });
        
        WriteLine("Products cost less than 10$");

        foreach(var p in selectionProducts)
        {
            WriteLine("{0}: {1} costs {2:$#,##0.00}", p.ProductId, p.ProductName, p.UnitPrice);
        }
        WriteLine("");

        WriteLine(selectionProducts.ToQueryString());
    }

    private static void JoinCategoriesAndProducts(){
        SectionTitle("JoinCategoriesAndProducts");

        using Northwind db = new();

        var queryJoin = db.Categories.Join(
            db.Products, category => category.CategoryId,
            product => product.CategoryId,
            (c,p) => new {c.CategoryName, p.ProductName, p.ProductId}
        ).OrderBy(cat=> cat.CategoryName);

        foreach(var output in queryJoin){
            WriteLine("{0}: {1} in category {2}", output.ProductId, output.ProductName, output.CategoryName);
        }
    }

    private static void GroupAndJoinCategoriesProducts(){
        SectionTitle("GroupAndJoinCategoriesProducts");

        using Northwind db = new();

        var groupQuery = db.Categories.AsEnumerable().GroupJoin(
          db.Products, cat => cat.CategoryId, prod=>prod.CategoryId,
          (cat, prods) => new{
            cat.CategoryName, Products = prods.OrderBy(p=>p.ProductName)
          }
        );

        foreach(var el in groupQuery){
            WriteLine("Category {0} has {1} products", el.CategoryName,el.Products.Count());
        }
    }

    private static void ProductsSearch()
    {
        SectionTitle("ProductsSearch");

        using Northwind db = new();

        var productQuery =db.Categories.Join(
            db.Products, cat=>cat.CategoryId, prod=>prod.CategoryId, (c,p) => new {c.CategoryName, Product = p}
        );

        ILookup<string, Product> productLookup = productQuery.ToLookup(
            cp=>cp.CategoryName,
            cp=>cp.Product
        );

        foreach(IGrouping<string,Product> group in productLookup)
        {
            WriteLine($"{group.Key} has {group.Count()} products.");
            foreach(var p in group)
            {
                WriteLine($"  {p.ProductName}");
            }
        }
        Write("Enter category name:");
        string categoryName =  ReadLine()!;

        WriteLine();

        WriteLine($"Products from category: {categoryName}:");
        IEnumerable<Product> productsFromCategory = productLookup[categoryName];

        foreach(var p in productsFromCategory){
            WriteLine($" {p.ProductName}");
        }
    }

    private static void AggregationProductTable(){
        SectionTitle("AggregationProducts");

        using var db = new Northwind();

        if(db.Products.TryGetNonEnumeratedCount(out int countDbSet))
        {
            WriteLine("{0,-25} {1,10}",
            "Count of products from DbSet",countDbSet);
        } else{
            WriteLine("No Count Property from DbSet");
        }

        List<Product> products=db.Products.ToList();

        if(products.TryGetNonEnumeratedCount(out int countList))
        {
             WriteLine("{0,-25} {1,10}",
            "Count of products from list",countList);
        } else{
            WriteLine("No count property");
        }

         WriteLine($"{"Product count:",-25} {db.Products.Count(),10}");

    WriteLine($"{"Discontinued product count:",-27} {db
      .Products.Count(product => product.Discontinued),8}");

    WriteLine($"{"Highest product price:",-25} {db
      .Products.Max(p => p.UnitPrice),10:$#,##0.00}");

    WriteLine($"{"Sum of units in stock:",-25} {db
      .Products.Sum(p => p.UnitsInStock),10:N0}");

    WriteLine($"{"Sum of units on order:",-25} {db
      .Products.Sum(p => p.UnitsOnOrder),10:N0}");

    WriteLine($"{"Average unit price:",-25} {db
      .Products.Average(p => p.UnitPrice),10:$#,##0.00}");

    WriteLine($"{"Value of units in stock:",-25} {db.Products
      .Sum(p => p.UnitPrice * p.UnitsInStock),10:$#,##0.00}");

    }

    private static void WriteProductsTable(Product[] products, int site, int siteCount){
        
        string row = new('-',73);
        string halfRow = new('-',30);

        WriteLine(row);

        WriteLine("{0,4} {1,-40} {2,12} {3,-15}","ID","ProductName","PRice","Uncon.");
        WriteLine(row);

        foreach(var p in products){
            WriteLine("{0,4} {1,-40} {2,12:C} {3,-15}",p.ProductId,p.ProductName,p.UnitPrice,p.Discontinued);
        }
        WriteLine("{0} page {1} from {2} {3}",halfRow, site + 1,siteCount+1, halfRow);
    }

    private static void WriteProductPage(IQueryable<Product> products, int pageSize, int actualPage, int pageCount)
    {
        var pagingQuery = products.OrderBy(p=>p.ProductId).Skip(actualPage * pageSize).Take(pageSize);

        Clear();

        SectionTitle(pagingQuery.ToQueryString());

        WriteProductsTable(pagingQuery.ToArray(),actualPage,pageCount);
        
    }

    private static void ProductsPaging(){
        SectionTitle("ProcutsPaging");

        using Northwind db = new();

        int pageSize = 10;
        int actualPage=0;
        int productsCount = db.Products.Count();
        int pageCount = productsCount/pageSize;

        while(true){
            WriteProductPage(db.Products,pageSize,actualPage,pageCount);

            Write("Previous page <- , next page ->, diffrent key to quit");
            ConsoleKey key = ReadKey().Key;

            if(key == ConsoleKey.LeftArrow){
                actualPage = actualPage == 0 ? pageCount : actualPage -1;
            } else if (key == ConsoleKey.RightArrow)
            {
                actualPage = actualPage == pageCount ? 0 : actualPage +1;
            }
            else
            {
                break;
            }
            WriteLine();
        }
    }
}