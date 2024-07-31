namespace Pinewood.CustomerApp.Host.Models;

public class Customer
{
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
}