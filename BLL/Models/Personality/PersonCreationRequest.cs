using DAL.Entities.Gym.Person;

namespace BLL.Models.Personality;

public class PersonCreationRequest
{
    public string Name { get; set; }
    public string? Surname { get; set; } 
    public string? Patronymic { get; set; }
    public string? Phone { get; set; }
    public string? Mail { get; set; }
    public DateOnly? BirthDate { get; set; }
    public Person.GenderType? Gender { get; set; }
}