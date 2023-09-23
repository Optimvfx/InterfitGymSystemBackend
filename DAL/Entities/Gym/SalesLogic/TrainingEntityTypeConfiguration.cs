using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.SalesLogic;

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