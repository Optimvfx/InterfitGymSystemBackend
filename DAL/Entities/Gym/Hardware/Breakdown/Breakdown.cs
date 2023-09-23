using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Entities.Gym.Hardware.Breakdown;

[Table("Breakdowns")]
public class Breakdown
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateOnly DetectedDate { get; set; }
    [AllowNull] public DateOnly? FixedDate { get; set; }
    
    [Required] public Guid TypeId { get; set; }
    public virtual BreakdownType Type { get; set; }
    
    [Required] public Guid DetectedAtId { get; set; }
    public virtual TrainingDevice DetectedAt { get; set; }
}