using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Hardware;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.SalesLogic;

[Table("Orders")]
public class Order : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public uint Payment { get; set; }
    [Required] public DateTime Date { get; set; }
    [Required] public bool Finished { get; set; }
    
    [Required] public Guid TypeId { get; set; }
    public virtual OrderType Type { get; set; }
    
    [Required] public Guid ExecutorId { get; set; }
    public virtual Company Executor { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}