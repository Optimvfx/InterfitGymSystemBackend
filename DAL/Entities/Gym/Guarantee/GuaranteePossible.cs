using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.Guarantee;

public abstract class GuaranteePossible : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
 
    [AllowNull] public Guid? GuaranteeId { get; set; }
    public virtual Guarantee? Guarantee { get; set; }
}