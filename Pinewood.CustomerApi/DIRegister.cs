using Microsoft.Extensions.DependencyInjection;
using Pinewood.CustomerApi.Handlers;
using Pinewood.CustomerApi.Repositories;

namespace Pinewood.CustomerApi;

public static class DIRegister
{
    public static void AddHandlers(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerHandler, CustomerHandler>();
    }

    // No longer used - left as example.
    public static void AddInMemoryRepositories(this IServiceCollection services)
    {
        services.AddSingleton<ICustomerRepository, InMemoryCustomerRepository>();
    }
}
