using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym;

public class GymTypeConfiguration : IEntityTypeConfiguration<Gym>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder.HasMany(g => g.Cards)
            .WithOne(c => c.Gym)
            .HasForeignKey(c => c.GymId)
            .OnDelete(DeleteBehavior.SetNull);

        #region SalesLogic

        builder.HasMany(g => g.TradeTransactions)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(g => g.Trainings)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasMany(g => g.Orders)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.Cascade);
        
        #endregion

        #region People

        builder.HasMany(g => g.Personnel)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(g => g.Coaches)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(g => g.Visitations)
            .WithOne(v => v.Gym)
            .HasForeignKey(v => v.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        #endregion
        
        #region Hardware
        
        builder.HasMany(g => g.TrainingDevices)
            .WithOne(c => c.Gym)
            .HasForeignKey(c => c.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(g => g.Consumables)
            .WithOne(c => c.Gym)
            .HasForeignKey(c => c.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(g => g.TechnicalHardware)
            .WithOne(c => c.Gym)
            .HasForeignKey(c => c.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        
        #endregion
        
        builder.HasMany(g => g.Terminals)
            .WithOne(c => c.Gym)
            .HasForeignKey(c => c.GymId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}