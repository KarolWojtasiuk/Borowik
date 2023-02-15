using DexieNET;

namespace Borowik.Database.Dexie.Entities;

public record BookDataEntity
(
    [property: Index(IsPrimary = true, IsUnique = true)] Guid BookId,
    [property: ByteIndex] byte[] Data
) : IBorowikEntityStore;
