using Pinewood.CustomerApp.Host.Models;
using System.Net.Http.Json;

namespace Pinewood.CustomerApp.Host;

public class CustomerService
{
    private readonly HttpClient _httpClient;

    public CustomerService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Customer>> GetCustomersAsync()
    {
        var customers = await _httpClient.GetFromJsonAsync<List<Customer>>("/api/customers");
        if (customers == null)
            customers = new List<Customer>();
        return customers;
    }

    public async Task<Customer> GetCustomerAsync(Guid customerId)
    {
        var customer = await _httpClient.GetFromJsonAsync<Customer>($"/api/customers/{customerId}");
        return customer!; // TODO: Ignoring not found for now. Would add logic for scenario in the future.
    }

    public async Task AddCustomerAsync(Customer customer)
    {
        await _httpClient.PostAsJsonAsync("/api/customers", customer);
    }

    public async Task UpdateCustomerAsync(Customer customer)
    {
        await _httpClient.PutAsJsonAsync($"/api/customers/{customer.CustomerId}", customer);
    }

    public async Task DeleteCustomerAsync(Guid customerId)
    {
        await _httpClient.DeleteAsync($"/api/customers/{customerId}");
    }
}
