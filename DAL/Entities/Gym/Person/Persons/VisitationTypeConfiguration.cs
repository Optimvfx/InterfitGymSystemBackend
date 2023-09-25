using System.Collections.Specialized;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Persons;

public class VisitationTypeConfiguration: IEntityTypeConfiguration<Visitation>
{
    public void Configure(EntityTypeBuilder<Visitation> builder)
    {
        builder.HasOne(v => v.Person)
            .WithMany(p => p.Visitations)
            .HasForeignKey(v => v.PersonId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(v => v.Gym)
            .WithMany(g => g.Visitations)
            .HasForeignKey(v => v.GymId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}