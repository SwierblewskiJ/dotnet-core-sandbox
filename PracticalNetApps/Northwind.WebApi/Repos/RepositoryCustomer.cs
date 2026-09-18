using EntityModels;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.EntityFrameworkCore;

namespace Northwind.WebApi.Repositories;

public class RepositoryCustomer : IRepositoryCustomer
{
    private readonly IMemoryCache _memoryCache;

    private readonly MemoryCacheEntryOptions _options = new()
    {
        SlidingExpiration = TimeSpan.FromSeconds(30),
    };

    private NorthwindContext _db;

    public RepositoryCustomer(NorthwindContext incjectedContext, IMemoryCache memoryCache)
    {
        _db = incjectedContext;
        _memoryCache = memoryCache;
    }

    public async Task<Customer?> CreateAsync(Customer c)
    {
        c.CustomerId = c.CustomerId.ToUpper();
        EntityEntry<Customer> added = await _db.Customers.AddAsync(c);
        int changed = await _db.SaveChangesAsync();
        if(changed == 1)
        {
            _memoryCache.Set(c.CustomerId, c, _options);
            return c;
        }
        return null;
    }

    public async Task<bool?> DeleteByIdAsync(string id)
    {
        id = id.ToUpper();
        Customer? c = await _db.Customers.FindAsync(id);
        if(c is null) return null;

        _db.Customers.Remove(c);
        int changed = await _db.SaveChangesAsync();
        if(changed == 1)
        {
            _memoryCache.Remove(c.CustomerId);
            return true;
        }
        return false;
    }

    public async Task<IEnumerable<Customer>> GetAllAsync()
    {
        return await _db.Customers.ToArrayAsync();
    }

    public Task<Customer?> GetOneById(string id)
    {
        id = id.ToUpper();

        if(_memoryCache.TryGetValue(id, out Customer? fromMemory))
        {
            return Task.FromResult(fromMemory);
        }

        Customer? fromDb = _db.Customers.FirstOrDefault(c=>c.CustomerId == id);

        if(fromDb is null) return Task.FromResult(fromDb);

        _memoryCache.Set(fromDb.CustomerId, fromDb, _options);
        return Task.FromResult(fromDb)!;
    }

    public async Task<Customer?> UpdateByIdAsync(string id, Customer c)
    {
       c.CustomerId = c.CustomerId.ToUpper();

       _db.Customers.Update(c);
       int changed = await _db.SaveChangesAsync();
       if(changed == 1)
        {
            _memoryCache.Set(c.CustomerId, c, _options);
            return c;
        } 
        return null;
    }
}