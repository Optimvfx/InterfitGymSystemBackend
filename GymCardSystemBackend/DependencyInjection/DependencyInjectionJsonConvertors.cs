using System.Text.Json.Serialization;
using GymCardSystemBackend.Json;

namespace GymCardSystemBackend.DependencyInjection;

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