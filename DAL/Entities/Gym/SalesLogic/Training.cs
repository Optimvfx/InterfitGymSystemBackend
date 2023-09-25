using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Person;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.SalesLogic;

[Table("Trainings")]
public class Training : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public DateTime Date { get; set; }
    [Required] public uint DurationInHours { get; set; }
    [Required] public uint PaymentPerHour { get; set; }
    
    [Required] public Guid ClientId { get; set; }
    public virtual Client Client { get; set; }
    
    [Required] public Guid TrainerId { get; set; }
    public virtual Trainer Trainer { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}