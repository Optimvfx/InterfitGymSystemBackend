namespace Common.Exceptions.General.NotFoundException;

public class ValueNotFoundByKeyException : ValueNotFoundedException
{
    public readonly string Value;
    public readonly string Key;

    public ValueNotFoundByKeyException(Type value, string key)
    {
        Value = value.Name;
        Key = key;
    }
    
    public ValueNotFoundByKeyException(string value, string key)
    {
        Value = value;
        Key = key;
    }

    public override object GetValue() => Value;

    public override object? GetKey() => Key;
    
    public override string Message => GetMessage();

    private string GetMessage()
    {
        return $"{Value} is not found by key({Key}).";
    }
}