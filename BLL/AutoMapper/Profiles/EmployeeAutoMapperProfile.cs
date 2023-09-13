using BLL.Models.Employee;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Employeers;

namespace BLL.AutoMapper.Profiles;

public class EmployeeAutoMapperProfile : BaseAutoMapperProfile
{
    public EmployeeAutoMapperProfile()
    {
        CreateMap<Timetable, TimetableEntity>();
        CreateMap<VacationCreationRequest, Vacation>();
        CreateMap<EmployeeCreationRequest, Employee>();
    }
}