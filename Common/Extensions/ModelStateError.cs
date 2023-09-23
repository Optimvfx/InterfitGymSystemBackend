namespace Common.Extensions;

public struct ModelStateError
{
    public string Key;
    public string ErrorMessage;

    public ModelStateError(string key, string errorMessage)
    {
        Key = key;
        ErrorMessage = errorMessage;
    }
}