using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Guarantee;

[Table("Guarantees")]
public class Guarantee : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateOnly ExpirationDate { get; set; }
    
    [Required] public Guid TargetId { get; set; }
    public virtual GuaranteePossible Target { get; set; }
    
    [Required] public Guid StoragePlaceId { get; set; }
    public virtual StoragePlace StoragePlace { get; set; }
}