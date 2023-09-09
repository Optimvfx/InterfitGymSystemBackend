using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Entities.Structs;

public struct DayGraphic
{
    [Required] public readonly TimeOnly StartWorkAt { get; }
    [Required] public readonly TimeOnly StopWorkAt { get; }

    public DayGraphic(TimeOnly startWorkAt, TimeOnly stopWorkAt)
    {
        if (startWorkAt > stopWorkAt)
            throw new AggregateException();
        
        StartWorkAt = startWorkAt;
        StopWorkAt = stopWorkAt;
    }
}

public class DayGraphicConverter : ValueConverter<DayGraphic, string>
{
    public DayGraphicConverter()
        : base(
            v => $"{v.StartWorkAt.ToString()},{v.StopWorkAt.ToString()}",
            v => GetByString(v)){}
    private static DayGraphic GetByString(string str)
    {
        var parts = str.Split(',');
        return new DayGraphic(TimeOnly.Parse(parts[0]), TimeOnly.Parse(parts[1]));
    }
}