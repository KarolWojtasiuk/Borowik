using System.Runtime.CompilerServices;
using Borowik.Books.Services;
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
            .AddMediatR(typeof(DependencyInjectionExtensions).Assembly)
            .AddSingleton<IQuerier, MediatorQuerier>()
            .AddSingleton<ICommander, MediatorCommander>()
            .AddSingleton<IGuidProvider, GuidProvider>()
            .AddSingleton<IDateTimeProvider, DateTimeProvider>()
            .AddTransient<IRawBookParser, RawBookParser>()
            .AddTransient<IRawBookTypeParser, PlainTextRawBookTypeParser>();
    }
}