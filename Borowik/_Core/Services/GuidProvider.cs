using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Services;

[ServiceDescriptor<IGuidProvider>(ServiceLifetime.Singleton)]
internal class GuidProvider : IGuidProvider
{
    public Guid Generate()
    {
        return Guid.NewGuid();
    }
}