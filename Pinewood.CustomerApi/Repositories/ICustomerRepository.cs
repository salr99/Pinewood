using Pinewood.CustomerApi.Models;

namespace Pinewood.CustomerApi.Repositories;

public interface ICustomerRepository
{
    IEnumerable<Customer> GetAll();
    Customer? GetById(Guid customerId);
    void Add(Customer customer);
    void Update(Customer customer);
    void Delete(Guid customerId);
}
