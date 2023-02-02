using System.Runtime.CompilerServices;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Borowik.Database.LiteDb")]

namespace Borowik.Database.LiteDb;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikLiteDb<TLiteDbProvider>(this IServiceCollection services, ServiceLifetime liteDbProviderLifetime = ServiceLifetime.Singleton)
        where TLiteDbProvider : ILiteDbProvider
    {
        LiteDbMapper.RegisterMappings();

        services.Add(new ServiceDescriptor(typeof(ILiteDbProvider), typeof(TLiteDbProvider), liteDbProviderLifetime));
        return services;
    }
}