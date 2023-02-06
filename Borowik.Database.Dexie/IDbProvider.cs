using Borowik.Database.Dexie.Entities;

namespace Borowik.Database.Dexie;

internal interface IDbProvider
{
    public Task<BorowikEntityStoreDB> GetAsync();
}