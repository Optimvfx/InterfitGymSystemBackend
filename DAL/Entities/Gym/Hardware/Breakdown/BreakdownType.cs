using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace DAL.Entities.Gym.Hardware.Breakdown;

[Table("BreakdownTypes")]
public class BreakdownType
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }

    public virtual ICollection<Breakdown> Breakdowns { get; set; }
}