namespace Borowik;

public abstract class BorowikException : Exception
{
    protected BorowikException(string? message) : base(message)
    {
    }
}