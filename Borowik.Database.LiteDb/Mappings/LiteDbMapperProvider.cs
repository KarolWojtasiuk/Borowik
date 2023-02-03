namespace Borowik.Database.LiteDb.Mappings;

internal class LiteDbMapperProvider : ILiteDbMapperProvider
{
    private readonly IBookContentPageSerializer _serializer;
    private LiteDbMapper? _mapper;

    public LiteDbMapperProvider(IBookContentPageSerializer serializer)
    {
        _serializer = serializer;
    }
    
    public LiteDbMapper Get()
    {
        return _mapper ??= new LiteDbMapper(_serializer);
    }
}