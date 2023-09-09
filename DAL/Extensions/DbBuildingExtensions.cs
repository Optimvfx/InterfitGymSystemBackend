using System.Reflection;
using DAL.Entities.Structs;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Extensions;

public static class DbBuildingExtensions
{
    public static ModelConfigurationBuilder UseCustomValueConverterSelector(this ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder
            .Properties<DayGraphic>()
            .HaveConversion<DayGraphicConverter>();
        
        return configurationBuilder;
    }

    public static ModelBuilder ApplyAllConfigurations(this ModelBuilder builder)
    {
        ApplyAllConfigurationsFromAssembly(builder);
        
        return builder;
    }
    
    private static void ApplyAllConfigurationsFromAssembly(this ModelBuilder modelBuilder)
    {
        var typesToRegister = Assembly.GetExecutingAssembly().GetTypes().Where(t => t.GetInterfaces()
            .Any(gi => gi.IsGenericType && gi.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))).ToList();

        foreach (var type in typesToRegister)
        {
            dynamic configurationInstance = Activator.CreateInstance(type);
            modelBuilder.ApplyConfiguration(configurationInstance);
        }
    }
}
