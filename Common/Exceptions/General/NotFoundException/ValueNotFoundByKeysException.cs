using System.Text;
using Common.Extensions;

namespace Common.Exceptions.General.NotFoundException;

public class ValueNotFoundByKeysException : ValueNotFoundedException
{
    public readonly string Value;
    public readonly IReadOnlyList<KeyInfo> KeyInfos;

    public ValueNotFoundByKeysException(string value, IEnumerable<KeyInfo> keyInfos)
    {
        if(keyInfos.Where(k => k.ExceptedValue != null)
           .Nothing(k => k.ExceptedValue != k.Value))
            throw new ArgumentException("At lest one excepted value must by different then real value");
        
        Value = value;
        KeyInfos = keyInfos.ToList();
    }

    public override object GetValue() => Value;

    public override object? GetKey() => KeyInfos;

    public override string Message => GetMessage();

    private string GetMessage()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{Value} is not found by keys:");

        foreach (var keyValue in KeyInfos)
        {
            if (keyValue.ExceptedValue == null)
                sb.AppendLine($"Key {keyValue.Key} and value {keyValue.Value}.");
            else
                sb.AppendLine($"Key {keyValue.Key} and value {keyValue.Value}, " +
                              $"excepted value {keyValue.ExceptedValue}.");
        }

        return sb.ToString();
    }
    
    public struct KeyInfo
    {
        public readonly string Key;
        public readonly string Value;
        public readonly string? ExceptedValue;

        public KeyInfo(string key, string value, string? exceptedValue = null)
        {
            Key = key;
            Value = value;
            ExceptedValue = exceptedValue;
        }
    }
}