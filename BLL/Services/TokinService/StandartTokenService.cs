using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DAL.Entities;
using DAL.Entities.Access;
using DAL.Entities.Access.AccessType;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services.TokinService;

public class StandartTokenService : ITokenService
{
    private readonly string _secretKey;
    private readonly int _tokenValidityHours;

    public StandartTokenService(string secretKey, int tokenValidityHours)
    {
        if (tokenValidityHours <= 0)
            throw new AggregateException();
        
        _secretKey = secretKey;
        _tokenValidityHours = tokenValidityHours;
    }
    public string GenerateJwtToken(Terminal terminal)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, terminal.Id.ToString())
            }),
            Expires = DateTime.UtcNow.AddHours(_tokenValidityHours),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}