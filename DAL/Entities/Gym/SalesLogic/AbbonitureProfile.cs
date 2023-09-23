using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Gym.SalesLogic;

[Table("AbbonitureProfiles")]
public class AbbonitureProfile : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }   
    
    [Required] public uint Price { get; set; }
    
    [AllowNull] public uint? VisitLimit { get; set; }
    [AllowNull] public uint? DateLimitInDays { get; set; }
    
    [Required] public bool OnSale { get; set; }
    
    public virtual ICollection<TradeTransaction> TradeTransactions { get; set; }
}