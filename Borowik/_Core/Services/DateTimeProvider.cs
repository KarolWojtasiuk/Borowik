namespace Borowik.Services;

internal class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetUtcNew()
    {
        return DateTime.UtcNow;
    }
}