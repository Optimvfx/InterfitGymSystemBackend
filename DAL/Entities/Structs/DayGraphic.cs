using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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