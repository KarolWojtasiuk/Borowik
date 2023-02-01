using System.Runtime.CompilerServices;
using Borowik.Books.Services;
using Borowik.Commands;
using Borowik.Queries;
using Borowik.Services;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

[assembly: InternalsVisibleTo("Borowik.Database.Sqlite")]

namespace Borowik;

public static class DependencyInjectionExtensions
{
    public static IServiceCollection AddBorowik(this IServiceCollection services)
    {
        return services
            .AddMediatR(typeof(DependencyInjectionExtensions).Assembly)
            .AddSingleton<ICommander, MediatorCommander>()
            .AddSingleton<IQuerier, MediatorQuerier>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddSingleton<IGuidProvider, GuidProvider>()
            .AddTransient<IRawBookParser, RawBookParser>()
            .AddTransient<IRawBookTypeParser, PlainTextRawBookTypeParser>();
    }
}