using BLL.Services.Abstract;
using Common.Extensions;
using DAL;
using DAL.Entities.Gym.Person.Employeers;
using Microsoft.Ajax.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BLL.Services;

public class PositionService : DbSetService<Position>
{
    public PositionService(ApplicationDbContext db) : base(db)
    {
    }

    public async Task<Guid> AddAsync(string title, string? description)
    {
        var position = new Position()
        {
            Title = title,
            Description = description
        };

        return await InnerAdd(position);
    }

    public async Task EditAsync(Guid id, string newTitle, string? newDescription = null)
    {
        var position = await GetAsTrackingAsync(id);
        
        position.Title = newTitle;
            
        if (newDescription != null)
            position.Description = newTitle;

        await Db.SaveChangesAsync();
    }
    
    public async Task DeleteAsync(Guid id) => await DeleteAsync(id);

    protected override DbSet<Position> GetDbSet(ApplicationDbContext dbContext) => dbContext.Positions;
}



