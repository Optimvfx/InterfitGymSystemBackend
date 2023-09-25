using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("Vactions")]
public class Vacation : IIndexSearchable
{
   [Key] public Guid Id { get; set; }
   
   [AllowNull] public string? Reson { get; set; }
   
   [Required] public DateOnly StartDate { get; set; }
   [Required] public DateOnly EndDate { get; set; }
   
   [Required] public Guid EmployeeId { get; set; }
   public virtual Employee Employee { get; set; }
}