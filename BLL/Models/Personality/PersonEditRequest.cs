using BLL.Models.Interface;
using DAL.Entities.Gym.Person;

namespace BLL.Models.Personality;

public class PersonEditRequest: IEditRequest<Person>
{
    public string? NewName { get; set; }  
    public string? NewSurname { get; set; } 
    public string? NewPatronymic { get; set; }
    public string? NewPhone { get; set; }
    public string? NewMail { get; set; }
    public DateOnly? NewBirthDate { get; set; }
    public Person.GenderType? NewGender { get; set; }
    
    public Person ApplyEdit(Person value)
    {
        if (NewName != null)
            value.Name = NewName;

        if (NewSurname != null)
            value.Surname = NewSurname;

        if (NewPatronymic != null)
            value.Patronymic = NewPatronymic;

        if (NewPhone != null)
            value.Phone = NewPhone;

        if (NewMail != null)
            value.Mail = NewMail;

        if (NewBirthDate != null)
            value.BirthDate = NewBirthDate.Value;

        if (NewGender != null)
            value.Gender = NewGender.Value;

        return value;
    }
}