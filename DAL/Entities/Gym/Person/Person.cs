using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Primary;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person;

[Table("Persons")]
public class Person
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Name { get; set; }
    [AllowNull] public string? Surname { get; set; } 
    [AllowNull] public string? Patronymic { get; set; }
    [Phone]
    [AllowNull] public string? Phone { get; set; }
    [EmailAddress]
    [AllowNull] public string? Mail { get; set; }
    [AllowNull] public DateOnly? BirthDate { get; set; }
    [AllowNull] public GenderType? Gender { get; set; }
    [AllowNull] public byte[] Photo { get; set; }
    
    public virtual ICollection<Card> Cards { get; set; }
    
    public enum GenderType
    {
        Male,
        Female
    }   
}

public class PersonTypeConfiguration : IEntityTypeConfiguration<Person>
{
    public void Configure(EntityTypeBuilder<Person> builder)
    {
        builder.HasMany(p => p.Cards)
            .WithOne(c => c.Owner)
            .HasForeignKey(c => c.OwnerId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}