using BLL.Services.Database;
using CLL.ControllersLogic.Interface;

namespace CLL.ControllersLogic;

public class PersonLogic : IPersonLogic
{
    private readonly PersonService _personService;

    public PersonLogic(PersonService personService)
    {
        _personService = personService;
    }

    public async Task<bool> Exists(Guid id)
    {
        return await _personService.Any(id);
    }
}