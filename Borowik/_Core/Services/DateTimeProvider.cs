using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Services;

[ServiceDescriptor<IDateTimeProvider>(ServiceLifetime.Singleton)]
internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNew()
    {
        return DateTime.UtcNow;
    }
}