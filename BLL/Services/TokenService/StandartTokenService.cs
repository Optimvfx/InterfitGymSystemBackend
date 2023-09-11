using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace BLL.Services.TokenService;

public class StandartTokenService : ITokenService
{
    private readonly SymmetricSecurityKey _secretKey;
    private readonly TimeSpan _tokenValidity;
    private readonly string _claimTitle;

    public StandartTokenService(SymmetricSecurityKey secretKey, TimeSpan tokenValidity, string claimTitle)
    {
        if (tokenValidity.Ticks <= 0)
            throw new ArgumentException();
        
        _secretKey = secretKey;
        _tokenValidity = tokenValidity;
        _claimTitle = claimTitle;
    }

    public string GenerateJwtToken(Guid id)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = _secretKey;
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(_claimTitle, id.ToString())
            }),
            Expires = DateTime.UtcNow.Add(_tokenValidity),
            SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}