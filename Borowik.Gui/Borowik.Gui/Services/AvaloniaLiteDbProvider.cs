using Borowik.Database.LiteDb;
using LiteDB;

namespace Borowik.Gui.Services;

internal class AvaloniaLiteDbProvider : ICustomLiteDbProvider
{
    public Task<LiteDatabase> GetLiteDatabase(BsonMapper mapper, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}