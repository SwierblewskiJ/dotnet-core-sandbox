using EntityModels;
using Microsoft.EntityFrameworkCore;

namespace Northwind.Blazor.Services;

public class NorthwindService : INorthwindService
{
    private readonly NorthwindContext db;

    public NorthwindService(NorthwindContext db)
    {
        this.db = db;
    }

    public Task<Customer> CreateClientAsync(Customer c)
    {
        db.Customers.Add(c);
        db.SaveChanges();
        return Task.FromResult(c);
    }

    public Task deleteCustomerAsync(string id)
    {
        Customer? customer = db.Customers.FirstOrDefaultAsync(c => c.CustomerId==id).Result;
        if(customer is null)
        {
            return Task.CompletedTask;
        } else
        {
            db.Customers.Remove(customer);
            return db.SaveChangesAsync();
        }
    }

    public Task<Customer?> GetCustomerAsync(string id)
    {
        return db.Customers.FirstOrDefaultAsync(c=>c.CustomerId == id);
    }

    public Task<List<Customer>> GetCustomersAsync(string country)
    {
        return db.Customers.Where(c=>c.Country == country).ToListAsync();
    }

    public Task<Customer> UpdateClientAsync(Customer c)
    {
        db.Entry(c).State = EntityState.Modified;
        db.SaveChanges();
        return Task.FromResult(c);
    }

    Task<List<Customer>> INorthwindService.GetCustomersAsync()
    {
        return db.Customers.ToListAsync();
    }
}