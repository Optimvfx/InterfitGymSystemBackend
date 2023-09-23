using System.Text.Json;
using System.Text.Json.Serialization;
using GymCardSystemBackend.Singleton;

namespace GymCardSystemBackend.Json;

public class GuidToStingConvertor : JsonConverter<Guid>
{
    public override Guid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.String)
        {
            var result = GuidCoderSingleton.GetGuidCoder().TryDecrypt(reader.GetString());
            
            if (result)
            {
                return result.Value;
            }
            else
            {
                throw new JsonException("Invalid Guid format");
            }
        }

        throw new JsonException("Expected a JSON string.");
    }

    public override void Write(Utf8JsonWriter writer, Guid value, JsonSerializerOptions options)
    {
        var valueString = GuidCoderSingleton.GetGuidCoder().Encrypt(value);
        writer.WriteStringValue(valueString);
    }
}