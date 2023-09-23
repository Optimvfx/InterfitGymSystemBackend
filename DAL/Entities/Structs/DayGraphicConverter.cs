using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DAL.Entities.Structs;

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