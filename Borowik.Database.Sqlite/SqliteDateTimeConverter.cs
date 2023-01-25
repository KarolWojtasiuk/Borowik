using System.Diagnostics;

namespace Borowik.Database.Sqlite;

internal class SqliteDateTimeConverter : ISqliteDateTimeConverter
{
    public DateTime Convert(long value)
    {
        return DateTime.SpecifyKind(new DateTime(value), DateTimeKind.Utc);
    }

    public long Convert(DateTime value)
    {
        Debug.Assert(value.Kind == DateTimeKind.Utc);

        return value.Ticks;
    }
}