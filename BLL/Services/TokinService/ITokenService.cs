using DAL.Entities;
using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;

namespace BLL.Services.TokinService;

public interface ITokenService
{
    string GenerateJwtToken(Terminal terminal);
}