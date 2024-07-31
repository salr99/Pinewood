using Pinewood.CustomerApi.Exceptions;
using Pinewood.CustomerApi.Models;
using Pinewood.CustomerApi.Repositories;
using Pinewood.CustomerApi.RequestsAndResponses;

namespace Pinewood.CustomerApi.Handlers;

public interface ICustomerHandler
{
    GetCustomersResponse GetAll();
    GetSetCustomerResponse GetById(GetCustomerRequest customer);
    GetSetCustomerResponse Add(CreateCustomerRequest customer);
    void Update(UpdateCustomerRequest customer);
    void Delete(DeleteCustomerRequest customer);
}

public class CustomerHandler : ICustomerHandler
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerHandler(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public GetCustomersResponse GetAll()
    {
        var customers = _customerRepository.GetAll();
        return new GetCustomersResponse
        {
            Customers = customers
        };
    }

    public GetSetCustomerResponse GetById(GetCustomerRequest request)
    {
        var customer = _customerRepository.GetById(request.CustomerId);
        if (customer == null)
            throw new NotFoundException();
        return new GetSetCustomerResponse(customer);
    }

    public GetSetCustomerResponse Add(CreateCustomerRequest request)
    {
        var customer = new Customer
        {
            CustomerId = Guid.NewGuid(),
            Name = request.Name,
            Email = request.Email
        };
        _customerRepository.Add(customer);
        return new GetSetCustomerResponse(customer);
    }

    public void Update(UpdateCustomerRequest request)
    {
        var customer = new Customer
        {
            CustomerId = request.CustomerId,
            Name = request.Name,
            Email = request.Email
        };
        _customerRepository.Update(customer);
    }

    public void Delete(DeleteCustomerRequest request)
    {
        _customerRepository.Delete(request.CustomerId);
    }
}