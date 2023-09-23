using BLL.Models.Employee;
using BLL.Models.Employee.Requests;
using DAL.Entities.Gym.Person;
using DAL.Entities.Gym.Person.Employeers;

namespace BLL.AutoMapper.Profiles;

public class EmployeeAutoMapperProfile : BaseAutoMapperProfile
{
    public EmployeeAutoMapperProfile()
    {
        CreateMap<EmployeeCreationRequest, Employee>();
    }
}