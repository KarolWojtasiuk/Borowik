using Borowik.Database.Dexie.Entities;
using DexieNET;
using Microsoft.Extensions.DependencyInjection;
using Scrutor;

namespace Borowik.Database.Dexie;

[ServiceDescriptor(typeof(IDbProvider), ServiceLifetime.Singleton)]
internal class DbProvider : IDbProvider
{
    private readonly IDexieNETService<BorowikEntityStoreDB> _dbService;

    public DbProvider(IDexieNETService<BorowikEntityStoreDB> dbService)
    {
        _dbService = dbService;
    }

    public async Task<BorowikEntityStoreDB> GetAsync()
    {
        // TODO: migrations, dexie.net currently does not provide current version info (only js prop but internal)
        var db = await _dbService.DexieNETFactory.Create();
        await db.Version(1).Stores();
        return db;
    }
}