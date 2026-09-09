using EFCoreLearn.Data;

using Northwind db = new();

Console.WriteLine($"Supplier: {db.Database.ProviderName}");