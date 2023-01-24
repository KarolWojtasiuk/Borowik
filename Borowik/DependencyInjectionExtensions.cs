using Borowik.Commands;
using Borowik.Queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Borowik;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowik(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(DependencyInjectionExtensions).Assembly)
            .AddSingleton<IQuerier, MediatorQuerier>()
            .AddSingleton<ICommander, MediatorCommander>();
    }
}