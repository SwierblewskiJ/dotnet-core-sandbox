using EntityModels;

namespace Northwind.UnitTests;

public class EntityModelsTests
{
    [Fact]
    public void TestConnectionWithDatabase()
    {
        using NorthwindContext db = new();
        Assert.True(db.Database.CanConnect());
    }

    [Fact]
    public void TestCategoryAmount()
    {
        using NorthwindContext db = new();
        int expected = 8;
        int actual=db.Categories.Count();

        Assert.Equal(expected, actual);

    }

    [Fact]
    public void TestIsProductId1Chai()
    {
        using NorthwindContext db = new();

        string expected = "Chai";
        Product? product = db.Products.Find(1);
        string actual = product?.ProductName ?? string.Empty;

        Assert.Equal(expected, actual);

    }
}
