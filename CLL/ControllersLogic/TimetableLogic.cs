using BLL.Models.Employee;
using BLL.Services;
using CLL.ControllersLogic.Interface.EmployeeLogic;
using DAL.Entities.Gym.Person.Employeers;

namespace CLL.ControllersLogic;

public class TimetableLogic : ITimetableLogic
{
    private readonly TimetableService _service;

    public TimetableLogic(TimetableService service)
    {
        _service = service;
    }

    public async Task<bool> ExistsTimetableAsync(Guid id) => await _service.AnyAsync(id);

    public async Task<TimetableEntity> GetTimetableAsync(Guid id) => await _service.GetAsync(id);

    public async Task<Guid> AddTimetableAsync(string title, Timetable timetable) =>
        await _service.AddAsync(title, timetable);

    public async Task EditTimetableAsync(Guid id, TimetableEditRequest request) =>
        await _service.EditAsync(id, request);

    public async Task EditTimetableAsync(Guid id, string newTitle, TimetableEditRequest request) =>
        await _service.EditAsync(id, request, newTitle);

    public async Task RemoveTimeTableAsync(Guid id) =>
        await _service.DeleteAsync(id);
}