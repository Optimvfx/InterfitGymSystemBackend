using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using DAL.Entities.Gym.SalesLogic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Primary;

[Table("ApiKeys")]
public class ApiKey
{
    [Key] public Guid Id { get; set; }
    
    [Required] public string Key { get; set; }
    
    [Required] public DateTime CreationDate { get; set; }   
    
    [Required] public uint DurationInDays { get; set; }
    
    [AllowNull] public string? Description { get; set; }
    
    [Required] public Guid AccessId { get; set; }
    public virtual Access.Access Access { get; set; }
    
    [Required] public Guid AuthorId { get; set; }
    public virtual ApiAdministrator Author { get; set; }
}

public class ApiKeyTypeConfiguration : IEntityTypeConfiguration<ApiKey>
{
    public void Configure(EntityTypeBuilder<ApiKey> builder)
    {
        builder.HasOne(a => a.Access)
            .WithMany(a => a.ApiKeys)
            .HasForeignKey(a => a.AccessId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(a => a.Author)
            .WithMany(a => a.CreatedApiKeys)
            .HasForeignKey(a => a.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}