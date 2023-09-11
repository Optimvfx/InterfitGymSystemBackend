using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

[Table("Vactions")]
public class Vacation : IIndexSearchable
{
   [Key] public Guid Id { get; set; }
   
   [AllowNull] public string? Reson { get; set; }
   [Required] public DateOnly CreationDate { get; set; }
   [Required] public uint DurationInDays { get; set; }
   
   [Required] public Guid EmployeeId { get; set; }
   public virtual Employee Employee { get; set; }
}

public class VacationTypeConfiguration : IEntityTypeConfiguration<Vacation>
{
   public void Configure(EntityTypeBuilder<Vacation> builder)
   {
      builder.HasOne(v => v.Employee)
         .WithOne(e => e.Vacation)
         .HasForeignKey<Vacation>(v => v.EmployeeId)
         .OnDelete(DeleteBehavior.SetNull);
   }
}