using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Guarantee;

namespace DAL.Entities.Gym.Hardware;

[Table("TechnicalHardwares")]
public class TechnicalHardware : GuaranteePossible
{
    [Required] public uint BuyPrice { get; set; }
    
    [Required] public Guid TechnicalHardwareInformationId { get; set; }
    public virtual TechnicalHardwareInformation TechnicalHardwareInformation { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}