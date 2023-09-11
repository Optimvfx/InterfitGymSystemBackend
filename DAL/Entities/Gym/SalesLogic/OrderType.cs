using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

[Table("OrderType")]
public class OrderType : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }
    
    public virtual ICollection<Order> Orders { get; set; }
}

public class OrderTypeEntityTypeConfiguration : IEntityTypeConfiguration<OrderType>
{
    public void Configure(EntityTypeBuilder<OrderType> builder)
    {
        builder.HasMany(t => t.Orders)
            .WithOne(o => o.Type)
            .HasForeignKey(o => o.TypeId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}