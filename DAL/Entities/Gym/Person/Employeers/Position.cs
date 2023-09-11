using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("Position")]
public class Position : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Title { get; set; }
    [AllowNull] public string? Description { get; set; }
    
    public virtual ICollection<Employee> Employeers { get; set; }
}

public class PositionTypeConfiguration : IEntityTypeConfiguration<Position>
{
    public void Configure(EntityTypeBuilder<Position> builder)
    {
        builder.HasMany(p => p.Employeers)
            .WithOne(e => e.Position)
            .HasForeignKey(e => e.PositionId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}