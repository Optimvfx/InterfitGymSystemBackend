using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Person.Persons;

[Table("Visitations")]
public class Visitation : IIndexSearchable
{
    public Guid Id { get; set; }

    [Required] public TimeOnly EnterTime { get; set; }
    [Required] public TimeOnly ExitTime { get; set; }
    [Required] public DateOnly Date { get; set; }
    
    [Required] public bool CorrectExit { get; set; }
    
    [Required] public Guid PersonId { get; set; }
    [Required] public virtual Person Person { get; set; }
    
    [AllowNull] public Guid? GymId { get; set; }
    public virtual Gym Gym { get; set; }
}