using Microsoft.Extensions.DependencyInjection;
using Pinewood.CustomerApi.Exceptions;
using Pinewood.CustomerApi.Models;
using Pinewood.CustomerApi.Repositories;

namespace Pinewood.Customers.EF;

public class CustomerRepository : ICustomerRepository
{
    private readonly IServiceScopeFactory _scopeFactory;

    public CustomerRepository(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public IEnumerable<Customer> GetAll()
    {
        using var scope = _scopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

        return context.Customers
            .Select(entity => entity.ToCustomer())
            .ToList();
    }

    public Customer? GetById(Guid customerId)
    {
        using var scope = _scopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

        var entity = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        return entity?.ToCustomer();
    }

    public void Add(Customer customer)
    {
        using var scope = _scopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();
        var entity = new CustomerEntity(customer);
        var utc = DateTime.UtcNow;
        entity.CreatedOn = utc;
        entity.ModifiedOn = utc;
        context.Customers.Add(entity);
        context.SaveChanges();
    }

    public void Update(Customer customer)
    {
        using var scope = _scopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

        var entity = context.Customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
        if (entity == null)
            throw new NotFoundException();

        entity.Name = customer.Name;
        entity.Email = customer.Email;
        entity.ModifiedOn = DateTime.UtcNow;

        context.Customers.Update(entity);
        context.SaveChanges();
    }

    public void Delete(Guid customerId)
    {
        using var scope = _scopeFactory.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<CustomerContext>();

        var entity = context.Customers.FirstOrDefault(c => c.CustomerId == customerId);
        if (entity == null)
            throw new NotFoundException();

        context.Customers.Remove(entity);
        context.SaveChanges();
    }
}
