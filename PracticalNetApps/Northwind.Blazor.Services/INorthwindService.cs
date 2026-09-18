using EntityModels;

namespace Northwind.Blazor.Services;

public interface INorthwindService
{
    Task<List<Customer>>GetCustomersAsync();
    Task<List<Customer>> GetCustomersAsync(string country);
    Task<Customer?> GetCustomerAsync(string id);
    Task<Customer> CreateClientAsync(Customer c);
    Task<Customer> UpdateClientAsync(Customer c);
    Task deleteCustomerAsync(string id);
}