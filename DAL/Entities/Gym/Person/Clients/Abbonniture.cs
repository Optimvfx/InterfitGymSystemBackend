using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Person.Clients;

[Table("Abbonnitures")]
public class Abbonniture : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public uint VisitsRemaining { get; set; }
    [Required] public DateOnly ExpirationDate { get; set; }
    
    public Guid ClientId { get; set; }
    public virtual Client Client { get; set; }
}