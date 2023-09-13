using AutoMapper;
using BLL.Models.Employee;
using BLL.Services.Abstract;
using Common.Extensions;
using DAL;
using DAL.Entities.Gym.Person.Employeers;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class TimetableService : DbSetService<TimetableEntity>
{
    private readonly IMapper _mapper;

    public TimetableService(ApplicationDbContext db, IMapper mapper) : base(db)
    {
        _mapper = mapper;
    }

    public async Task<Guid> AddAsync(string title, Timetable timetable)
    {
        var entity = _mapper.Map<TimetableEntity>(timetable);
        entity.Title = title;

        return await InnerAdd(entity);
    }

    public async Task EditAsync(Guid id, TimetableEditRequest newTimetable, string? newTitle = null)
    {
        var entiy = await GetAsTrackingAsync(id);

        newTimetable.ApplyEdit(entiy);

        if (newTitle != null)
            entiy.Title = newTitle;

        await Db.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id) => await DeleteAsync(id);
    
    protected override DbSet<TimetableEntity> GetDbSet(ApplicationDbContext dbContext) => dbContext.Timetables;
}



