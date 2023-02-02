namespace Borowik.Services;

internal class GuidProvider : IGuidProvider
{
    public Guid Generate()
    {
        return Guid.NewGuid();
    }
}