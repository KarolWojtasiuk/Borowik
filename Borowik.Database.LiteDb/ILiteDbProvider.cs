using LiteDB;

namespace Borowik.Database.LiteDb;

public interface ILiteDbProvider
{
    public Task<LiteDatabase> GetLiteDatabase(CancellationToken cancellationToken);

}