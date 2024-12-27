using Easy.Application.Interfaces;
using System.IdentityModel.Tokens.Jwt;

public class JwtService : IJwtService
{
    public string? ExtrairIdUsuario(string authorizationHeader)
    {
        if (string.IsNullOrEmpty(authorizationHeader) || !authorizationHeader.StartsWith("Bearer "))
        {
            return null;
        }

        var token = authorizationHeader.Substring("Bearer ".Length).Trim();
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);

        return jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
    }
}
