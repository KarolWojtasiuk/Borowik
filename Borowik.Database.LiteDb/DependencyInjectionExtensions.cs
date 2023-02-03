using Borowik.Books;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik.Database.LiteDb;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowikLiteDb<TCustomLiteDbProvider>(this IServiceCollection services)
        where TCustomLiteDbProvider : class, ICustomLiteDbProvider
    {
        return services.AddSingleton<LiteDbMapper>()
            .AddSingleton<ILiteDbProvider, LiteDbProvider>()
            .AddSingleton<ICustomLiteDbProvider, TCustomLiteDbProvider>()
            .AddTransient<IBookContentPageSerializer, BookContentPageSerializer>()
            .AddTransient<IBookshelfRepository, LiteDbBookshelfRepository>();
    }
}