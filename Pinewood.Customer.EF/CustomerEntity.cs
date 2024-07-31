using Pinewood.CustomerApi.Models;
using System.ComponentModel.DataAnnotations;

namespace Pinewood.Customers.EF;

public class CustomerEntity
{
    [Key]
    public Guid CustomerId { get; set; }
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public DateTime CreatedOn { get; set; }
    public DateTime ModifiedOn { get; set; }

    /// <summary>
    /// Default ctor for serialization suppport
    /// </summary>
    public CustomerEntity() { }

    // These two converters make sure that the internals of the storage implementation do not spill into the domain.

    // Converts a domain object to a storage entity.
    internal CustomerEntity(Customer customer)
    {
        CustomerId = customer.CustomerId;
        Name = customer.Name;
        Email = customer.Email;
        CreatedOn = customer.CreatedOn;
        ModifiedOn = customer.ModifiedOn;
    }

    // Converts a storage entity to a domain object.
    internal Customer ToCustomer()
    {
        return new Customer()
        {
            CustomerId = CustomerId,
            Name = Name,
            Email = Email,
            CreatedOn = CreatedOn,
            ModifiedOn = ModifiedOn,
        };
    }

    public static class Lengths
    {
        public const int Name = 255;
        public const int Email = 255;
    }
}