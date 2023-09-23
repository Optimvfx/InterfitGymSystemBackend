using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Person.Clients;

namespace DAL.Entities.Gym.Person;

[Table("Clients")]
public class Client : Person
{
    [AllowNull] public Guid? AbbonnitureId { get; set; }
    public virtual Abbonniture Abbonniture { get; set; }
    
    [AllowNull] public string? TelegramId { get; set; }
}