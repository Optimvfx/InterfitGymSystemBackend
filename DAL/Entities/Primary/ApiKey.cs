using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym.SalesLogic;
using DAL.Entities.Interfaces;

namespace DAL.Entities.Primary;

[Table("ApiKeys")]
public class ApiKey : IIndexSearchable
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Key { get; set; }
    
    [Required] public DateTime CreationDate { get; set; }   
    
    [Required] public uint DurationInDays { get; set; }
    
    [AllowNull] public string? Description { get; set; }
    
    [Required] public Guid AccessId { get; set; }
    public virtual Access.Access Access { get; set; }
    
    [AllowNull] public Guid? AuthorId { get; set; }
    public virtual ApiAdministrator? Author { get; set; }
}