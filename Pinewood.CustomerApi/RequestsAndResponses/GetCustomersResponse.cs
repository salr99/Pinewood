using Pinewood.CustomerApi.Models;

namespace Pinewood.CustomerApi.RequestsAndResponses;

public class GetCustomersResponse
{
    public IEnumerable<Customer> Customers { get; set; } = new List<Customer>();
}
