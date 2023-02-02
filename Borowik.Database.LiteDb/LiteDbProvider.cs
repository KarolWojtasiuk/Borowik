using LiteDB;

namespace Borowik.Database.LiteDb;

internal class LiteDbProvider : ILiteDbProvider
{
    private readonly ICustomLiteDbProvider _customLiteDbProvider;
    private readonly LiteDbMapper _mapper;

    public LiteDbProvider(
        ICustomLiteDbProvider customLiteDbProvider,
        LiteDbMapper mapper)
    {
        _customLiteDbProvider = customLiteDbProvider;
        _mapper = mapper;
    }

    public async Task<LiteDatabase> GetLiteDatabase(CancellationToken cancellationToken)
    {
        var db = await _customLiteDbProvider.GetLiteDatabase(_mapper, cancellationToken);

        if (db.Mapper != _mapper)
            throw new InvalidOperationException(
                "Pass provided 'mapper' parameter to LiteDatabase constructor in your ILiteDbProvider implementation");

        return db;
    }
}