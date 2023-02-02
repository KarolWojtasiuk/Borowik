using LiteDB;

namespace Borowik.Database.LiteDb;

public interface ICustomLiteDbProvider
{
    public Task<LiteDatabase> GetLiteDatabase(BsonMapper mapper, CancellationToken cancellationToken);
}