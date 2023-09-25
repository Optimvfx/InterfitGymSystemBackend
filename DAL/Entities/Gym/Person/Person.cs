using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Gym.Person.Persons;
using DAL.Entities.Interfaces;
using DAL.Entities.Primary;

namespace DAL.Entities.Gym.Person;

[Table("Persons")]
public class Person : IIndexSearchable
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
    
    public virtual ICollection<Visitation> Visitations { get; set; }
    public virtual ICollection<Card> Cards { get; set; }
    
    public enum GenderType
    {
        Male,
        Female
    }   
}