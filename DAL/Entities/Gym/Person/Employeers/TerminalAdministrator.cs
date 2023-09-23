using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Access.AccessType;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("TerminalAdministrators")]
public class TerminalAdministrator : Position
{
    [Required] public Guid TerminalId { get; set; }
    public virtual Terminal Terminal { get; set; }
}