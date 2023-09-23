using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Hardware;

[Table("Consumables")]
public class Consumable : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public uint Count { get; set; }
    [Required] public uint AvgBuyPrice { get; set; }
    
    [Required] public Guid ConsumableInformationId { get; set; }
    public virtual ConsumableInformation ConsumableInformation { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}