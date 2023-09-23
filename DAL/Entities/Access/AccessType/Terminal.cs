using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities._Configuration;
using DAL.Entities.Gym.Person.Employeers;

namespace DAL.Entities.Access.AccessType;

[Table("Terminals")]
public class Terminal : Access
{
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }
    
    [Required] public Guid AdministratorId { get; set; }
    public TerminalAdministrator Administrator { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym.Gym Gym { get; set; }
}