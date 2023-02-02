using LiteDB;

namespace Borowik.Database.LiteDb;

internal interface ILiteDbProvider
{
    public Task<LiteDatabase> GetLiteDatabase(CancellationToken cancellationToken);
}