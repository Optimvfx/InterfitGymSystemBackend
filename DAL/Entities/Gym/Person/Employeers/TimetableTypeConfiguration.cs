using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

public class TimetableTypeConfiguration : IEntityTypeConfiguration<TimetableEntity>
{
    public void Configure(EntityTypeBuilder<TimetableEntity> builder)
    {
        builder.HasMany(t => t.Employee)
            .WithOne(e => e.Timetable)
            .HasForeignKey(e => e.TimetableId)
            .OnDelete(DeleteBehavior.SetNull);
    }
}