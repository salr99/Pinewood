using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pinewood.Customers.EF;

public class CustomerContext : DbContext
{
    public CustomerContext(DbContextOptions<CustomerContext> options) : base(options)
    {
    }
    public DbSet<CustomerEntity> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerEntity>(b =>
        {
            b.HasKey(b => b.CustomerId)
                .IsClustered(false);

            b.Property(x => x.CustomerId)
                .IsRequired();

            b.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(CustomerEntity.Lengths.Name);

            b.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(CustomerEntity.Lengths.Email);

            b.Property(x => x.CreatedOn)
                .IsRequired();

            b.Property(x => x.ModifiedOn)
                .IsRequired();
        });
    }
}

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<CustomerContext>
{
    public CustomerContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<CustomerContext>();
        optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Customers;Trusted_Connection=True;"); // TODO: Make connection string come from app settings.

        return new CustomerContext(optionsBuilder.Options);
    }
}
