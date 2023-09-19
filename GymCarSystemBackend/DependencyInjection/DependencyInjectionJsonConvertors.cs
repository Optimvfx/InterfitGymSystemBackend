using System.Text.Json.Serialization;
using GymCarSystemBackend.Json;

namespace GymCarSystemBackend.DependencyInjection;

public static class DependencyInjectionJsonConvertors
{
    public static IMvcBuilder AddCustomJsonConvetors(this IMvcBuilder builder)
    {
        builder.AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.AddCustom();
        });

        return builder; 
    }

    private static void AddCustom(this IList<JsonConverter> contents)
    {
        contents.Add(new GuidToStingConvertor());
    }
}