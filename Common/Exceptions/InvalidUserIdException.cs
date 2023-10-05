using Common.Exceptions.General.NotFoundException;

namespace Common.Exceptions;

public class InvalidUserIdException : ValueNotFoundedException
{
    private readonly Guid? _id;
    private readonly Type _userType;

    public InvalidUserIdException(Type userType, Guid? id = null)
    {
        _id = id;
        _userType = userType;
    }

    public override object GetValue() => _userType;

    public override object? GetKey() => _id;
}