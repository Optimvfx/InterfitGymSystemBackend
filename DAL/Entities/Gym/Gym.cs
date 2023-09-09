using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym.Hardware;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.SalesLogic;
using DAL.Entities.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym;

[Table("Gyms")]
public class Gym
{
    [Required] public string Title { get; set; }
    
    //Cards of all personal trainers and clients
    public virtual ICollection<Card> Cards { get; set; }
    
    //Section of sales logic
    public virtual ICollection<AbbonitureProfile> AbbonitureProfiles { get; set; }
    public virtual ICollection<TradeTransaction> TradeTransactions { get; set; }
    public virtual ICollection<Training> Trainings { get; set; }
    public virtual ICollection<Order> Orders { get; set; }
    
    //People section
    public virtual ICollection<Employee> Personnel { get; set; }
    public virtual ICollection<Coach> Coaches { get; set; }
    public virtual ICollection<Client> Clients { get; set; }
    
    //Hardware managmet section
    public virtual ICollection<TrainingDevice> TrainingDevices { get; set; }
    public virtual ICollection<Consumable> Consumables { get; set; }
    public virtual ICollection<TechnicalHardware> TechnicalHardware { get; set; }
    
    public virtual ICollection<Terminal> Terminals { get; set; }
}

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
        builder.HasMany(g => g.AbbonitureProfiles)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.Cascade);

        #endregion

        #region People

        builder.HasMany(g => g.Personnel)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(g => g.Clients)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
            .OnDelete(DeleteBehavior.SetNull);
        builder.HasMany(g => g.Coaches)
            .WithOne(e => e.Gym)
            .HasForeignKey(e => e.GymId)
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