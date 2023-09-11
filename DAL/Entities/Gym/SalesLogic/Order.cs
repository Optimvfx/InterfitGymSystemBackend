using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Hardware;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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

public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasOne(o => o.Executor)
            .WithOne()
            .HasForeignKey<Order>(o => o.ExecutorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(o => o.Type)
            .WithOne()
            .HasForeignKey<Order>(o => o.TypeId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Orders)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}