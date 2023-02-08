using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Services;

[ServiceDescriptor(typeof(IDateTimeProvider), ServiceLifetime.Singleton)]
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNew()
    {
        return DateTime.UtcNow;
    }
}