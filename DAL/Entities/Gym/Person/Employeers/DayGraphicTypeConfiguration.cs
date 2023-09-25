using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DAL.Entities.Gym.Person.Employeers;

public class DayGraphicTypeConfiguration : IEntityTypeConfiguration<DayGraphic>
{
    public void Configure(EntityTypeBuilder<DayGraphic> builder)
    {
        builder.HasOne(g => g.Timetable)
            .WithMany(g => g.DayGraphics)
            .HasForeignKey(g => g.TimetableId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasKey(g => new {g.TimetableId, g.DayOfWeek});
    }
}