using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.Person;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

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
    public virtual Coach Trainer { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class TrainingEntityTypeConfiguration : IEntityTypeConfiguration<Training>
{
    public void Configure(EntityTypeBuilder<Training> builder)
    {
        builder.HasOne(t => t.Client)
            .WithOne()
            .HasForeignKey<Training>(t => t.ClientId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Trainer)
            .WithOne()
            .HasForeignKey<Training>(t => t.TrainerId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Trainings)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}