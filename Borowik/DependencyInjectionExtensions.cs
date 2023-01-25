using System.Runtime.CompilerServices;
using Borowik.Commands;
using Borowik.Queries;
using Borowik.Services;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

[assembly: InternalsVisibleTo("Borowik.Database.Sqlite")]

namespace Borowik;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowik(this IServiceCollection services)
    {
        return services
            .AddSingleton<IGuidProvider, GuidProvider>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddMediatR(typeof(DependencyInjectionExtensions).Assembly)
            .AddSingleton<IQuerier, MediatorQuerier>()
            .AddSingleton<ICommander, MediatorCommander>();
    }
}