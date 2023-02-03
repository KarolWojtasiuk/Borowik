using Borowik.Database.LiteDb.Mappings;
using LiteDB;

namespace Borowik.Database.LiteDb;

internal class LiteDbProvider : ILiteDbProvider
{
    private readonly ICustomLiteDbProvider _customLiteDbProvider;
    private readonly ILiteDbMapperProvider _mapperProvider;
    private LiteDbMapper? _mapper;

    public LiteDbProvider(
        ICustomLiteDbProvider customLiteDbProvider,
        ILiteDbMapperProvider mapperProvider)
    {
        _customLiteDbProvider = customLiteDbProvider;
        _mapperProvider = mapperProvider;
    }

    public async Task<LiteDatabase> GetLiteDatabase(CancellationToken cancellationToken)
    {
        _mapper ??= _mapperProvider.Get();

        var db = await _customLiteDbProvider.GetLiteDatabase(_mapper, cancellationToken);

        if (db.Mapper != _mapper)
            throw new InvalidOperationException(
                "Pass provided 'mapper' parameter to LiteDatabase constructor in your ILiteDbProvider implementation");

        return db;
    }
}