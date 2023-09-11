namespace BLL.Services.TokenService;

public interface ITokenService
{
    string GenerateJwtToken(Guid id);
}