using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Services;

[ServiceDescriptor(typeof(IGuidProvider), ServiceLifetime.Singleton)]
internal class GuidProvider : IGuidProvider
{
    public Guid Generate()
    {
        return Guid.NewGuid();
    }
}