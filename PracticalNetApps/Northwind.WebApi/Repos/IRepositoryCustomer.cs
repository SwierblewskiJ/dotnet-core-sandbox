using EntityModels;

namespace Northwind.WebApi.Repositories;

public interface IRepositoryCustomer
{
    Task<Customer?> CreateAsync(Customer c);
    Task<IEnumerable<Customer>> GetAllAsync();
    Task<Customer?> GetOneById(string id);
    Task<Customer?> UpdateByIdAsync(string id, Customer c);
    Task<bool?> DeleteByIdAsync(string id);
}