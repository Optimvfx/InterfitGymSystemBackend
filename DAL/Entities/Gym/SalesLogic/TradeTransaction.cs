using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Person;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.SalesLogic;

[Table("TradeTransactions")]
public class TradeTransaction : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateTime DateTime { get; set; }
    
    [Required] public Guid AbbonitureProfileId { get; set; }
    public virtual AbbonitureProfile AbbonitureProfile { get; set; }
    
    [Required] public Guid ClientId { get; set; }
    public virtual Client Client { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}