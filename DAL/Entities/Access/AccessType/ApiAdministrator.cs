using System.ComponentModel.DataAnnotations.Schema;
using DAL.Entities.Primary;

namespace DAL.Entities.Access.AccessType;

[Table("ApiAdministrator")]
public class ApiAdministrator : Access
{
    public virtual ICollection<ApiKey> CreatedApiKeys { get; set; }
}