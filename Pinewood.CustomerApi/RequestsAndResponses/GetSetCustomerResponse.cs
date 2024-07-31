using Pinewood.CustomerApi.Models;

namespace Pinewood.CustomerApi.RequestsAndResponses;

public class GetSetCustomerResponse
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;

    public GetSetCustomerResponse(Customer customer)
    {
        CustomerId = customer.CustomerId;
        Name = customer.Name;
        Email = customer.Email;
    }
}
