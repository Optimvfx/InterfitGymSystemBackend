using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym.Hardware;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.SalesLogic;
using DAL.Entities.Primary;

namespace DAL.Entities.Gym;

[Table("Gyms")]
public class Gym
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    
    //Cards of all personal trainers and clients
    public virtual ICollection<Card> Cards { get; set; }
    
    //Section of sales logic
    public virtual ICollection<TradeTransaction> TradeTransactions { get; set; }
    public virtual ICollection<Training> Trainings { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    
    //People section
    public virtual ICollection<Employee> Personnel { get; set; }
    public virtual ICollection<Coach> Coaches { get; set; }
    
    //Hardware managmet section
    public virtual ICollection<TrainingDevice> TrainingDevices { get; set; }
    public virtual ICollection<Consumable> Consumables { get; set; }
    public virtual ICollection<TechnicalHardware> TechnicalHardware { get; set; }
    
    public virtual ICollection<Terminal> Terminals { get; set; }
}