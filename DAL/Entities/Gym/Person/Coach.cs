using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Gym.SalesLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

[Table("Coaches")]
public class Coach : Person
{
    [Required] public uint TrainingPricePerHour { get; set; }
    
    public virtual ICollection<Training> Trainings { get; set; }
    
    [Required] public Guid GymId { get; set; }
    public virtual Gym Gym { get; set; }
}

public class CoachTypeConfiguration : IEntityTypeConfiguration<Coach>
{
    public void Configure(EntityTypeBuilder<Coach> builder)
    {
        builder.HasMany(c => c.Trainings)
            .WithOne(t => t.Trainer)
            .HasForeignKey(t => t.TrainerId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(t => t.Gym)
            .WithMany(g => g.Coaches)
            .HasForeignKey(t => t.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}