using System.Text.Json.Serialization;

namespace Pinewood.CustomerApi.RequestsAndResponses;

public class UpdateCustomerRequest
{
    [JsonIgnore]
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
