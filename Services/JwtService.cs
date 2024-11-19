using System.Text;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace api_rest_cs.Services;

public class JwtService
{
    public string GenerateToken(string userId, string username)
    {
        var keyString = Environment.GetEnvironmentVariable("JWT_KEY") ?? throw new ArgumentNullException("JWT_KEY", "JWT key is missing in environment variables.");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyString));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, userId),
            new Claim(JwtRegisteredClaimNames.UniqueName, username),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            claims: claims,
            issuer: Environment.GetEnvironmentVariable("JWT_ISSUER"),
            signingCredentials: credentials,
            audience: Environment.GetEnvironmentVariable("JWT_AUDIENCE"),
            expires: DateTime.UtcNow.AddMinutes(int.Parse(Environment.GetEnvironmentVariable("JWT_DURATION") ?? throw new ArgumentNullException("JWT_DURATION", "JWT duration is missing in environment variables.")))
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
