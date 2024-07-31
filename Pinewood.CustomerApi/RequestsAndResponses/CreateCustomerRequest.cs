namespace Pinewood.CustomerApi.RequestsAndResponses;

public class CreateCustomerRequest
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}
