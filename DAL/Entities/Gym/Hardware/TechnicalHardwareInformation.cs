using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Hardware;

[Table("TechnicalHardwaresInformation")]
public class TechnicalHardwareInformation : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }
    
    [Required] public Guid ManufacturerId { get; set; }
    public virtual Company Manufacturer { get; set; }
}