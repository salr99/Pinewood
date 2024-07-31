using Pinewood.CustomerApi.Exceptions;
using Pinewood.CustomerApi.Models;

namespace Pinewood.CustomerApi.Repositories;

// No longer used left as example. - using SQL Database now within Pinewood.Customers.EF.
public class InMemoryCustomerRepository : ICustomerRepository
{
    private readonly List<Customer> _customers = new List<Customer>()
    {
        new Customer(){ CustomerId = Guid.NewGuid(), Email = "CustomerOne@hotmail.com", Name="Bob" },
        new Customer(){ CustomerId = Guid.NewGuid(), Email = "CustomerTwo@hotmail.com", Name="Terry" },
        new Customer(){ CustomerId = Guid.NewGuid(), Email = "CustomerThree@hotmail.com", Name="Sam" },
    };

    public IEnumerable<Customer> GetAll() => _customers;

    public Customer? GetById(Guid customerId) => _customers.FirstOrDefault(c => c.CustomerId == customerId)!;

    public void Add(Customer customer)
    {
        _customers.Add(customer);
    }

    public void Update(Customer customer)
    {
        var existingCustomer = GetById(customer.CustomerId);

        if (existingCustomer == null)
            throw new NotFoundException();

        existingCustomer.Name = customer.Name;
        existingCustomer.Email = customer.Email;
    }

    public void Delete(Guid customerId)
    {
        var customer = GetById(customerId);
        if (customer == null)
            throw new NotFoundException();

        _customers.Remove(customer);
    }
}