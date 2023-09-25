using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.SalesLogic;

namespace DAL.Entities.Gym.Person;

[Table("Coaches")]
public class Trainer : Person
{
    [Required] public uint TrainingPricePerHour { get; set; }
    
    public virtual ICollection<Training> Trainings { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}