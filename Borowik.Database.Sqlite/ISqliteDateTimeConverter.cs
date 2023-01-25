namespace Borowik.Database.Sqlite;

public interface ISqliteDateTimeConverter
{
    public DateTime Convert(long value);
    public long Convert(DateTime value);
}