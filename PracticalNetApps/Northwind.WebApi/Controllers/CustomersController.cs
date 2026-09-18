using Microsoft.AspNetCore.Mvc;
using EntityModels;
using Northwind.WebApi.Repositories;

namespace Northwind.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomersController : ControllerBase
{
    private readonly IRepositoryCustomer _repo;

    public CustomersController(IRepositoryCustomer repo)
    {
        _repo=repo;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> ReadCustomersFromCountry(string? country)
    {
        if (string.IsNullOrWhiteSpace(country))
        {
            return await _repo.GetAllAsync();
        } else
        {
            return (await _repo.GetAllAsync()).Where(customer => customer.Country == country);
        }
    }

    [HttpGet("{id}", Name ="GetUserById")]
    [ProducesResponseType(200, Type = typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> ReadCustomerById(string id)
    {
        Customer? customer = await _repo.GetOneById(id);
        if(customer == null)
        {
            return NotFound();
        }

        return Ok(customer);
    }

    [HttpPost]
    [ProducesResponseType(201, Type=typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> CreateCustomer([FromBody] Customer c)
    {
        if(c is null)
        {
            return BadRequest();
        }

        Customer? addedCustomer = await _repo.CreateAsync(c);

        if(addedCustomer == null)
        {
            return BadRequest("Repository did not create client");
        } 

        return CreatedAtRoute( "GetUserById", new {id = addedCustomer.CustomerId}, addedCustomer);
        
    }

    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> UpdateCustomer(string id, [FromBody] Customer c)
    {
        id = id.ToUpper();
        c.CustomerId= c.CustomerId.ToUpper();

        if(c is null || c.CustomerId != id)
        {
            return BadRequest("");
        } 

        Customer? currentCustomer = await _repo.GetOneById(id);

        if(currentCustomer == null)
        {
            return NotFound();
        }

        await _repo.UpdateByIdAsync(id, c);
        return new NoContentResult();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> DeleteCustomer(string id)
    {
        if(id == "bad")
        {
            ProblemDetails problemDetails = new()
            {
                Status=StatusCodes.Status400BadRequest,
                Type="https://localhost:5151/customers/cannot-delete",
                Title=$"Customer {id} has been found, but cannot be deleted",
                Detail="additional details such as: companyName, country etc.",
                Instance=HttpContext.Request.Path
            };
            return BadRequest(problemDetails);
        }

        Customer? currentCustomer = await _repo.GetOneById(id);

        if(currentCustomer is null) return NotFound();

        bool? isDeleted = await _repo.DeleteByIdAsync(id);

        if(isDeleted.HasValue && isDeleted.Value)
        {
            return new NoContentResult();
        }
        else
        {
            return BadRequest($"Customer {id} has been found, but cannot cannot be deleted");
        }
    }
    


}