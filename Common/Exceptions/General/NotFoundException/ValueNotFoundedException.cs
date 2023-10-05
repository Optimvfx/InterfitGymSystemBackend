namespace Common.Exceptions.General.NotFoundException;

public abstract class ValueNotFoundedException : Exception
{
    public abstract object GetValue();
    public abstract object? GetKey();
}