using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

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