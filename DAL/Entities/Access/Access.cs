using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Interfaces;
using DAL.Entities.Primary;

namespace DAL.Entities.Access;

[Table("Access")]
public class Access : IIndexSearchable
{
    [Key] public Guid Id { get; set; }

    [Required] public ICollection<ApiKey> ApiKeys { get; set; } = null!;
}