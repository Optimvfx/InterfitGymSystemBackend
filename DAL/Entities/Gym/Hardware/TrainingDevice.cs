using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Guarantee;

namespace DAL.Entities.Gym.Hardware;

[Table("TrainingDevices")]
public class TrainingDevice : GuaranteePossible
{
    [Required] public uint BuyPrice { get; set; }
    
    [Required] public Guid TrainingDeviceInformationId { get; set; }
    public virtual TrainingDeviceInformation TrainingDeviceInformation { get; set; }
    
    public virtual ICollection<Breakdown.Breakdown> Breakdowns { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}