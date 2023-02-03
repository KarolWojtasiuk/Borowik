namespace Borowik.Database.LiteDb.Mappings;

internal interface ILiteDbMapperProvider
{
    public LiteDbMapper Get();
}