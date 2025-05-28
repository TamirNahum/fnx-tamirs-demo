using FnxTamirsServer.Services.Interfaces;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FnxTamirsServer.Services.Implementaions
{
  public class AuthService : IAuthService
  {
    private readonly IConfiguration _configuration;

    public AuthService(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    public async Task<string> GenerateToken(string username)
    {
      var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
      var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

      var claims = new[]
      {
                new Claim(ClaimTypes.Name, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

      var token = new JwtSecurityToken(
          issuer: _configuration["Jwt:Issuer"],
          audience: _configuration["Jwt:Audience"],
          claims: claims,
          expires: DateTime.Now.AddHours(24),
          signingCredentials: credentials
      );

      return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidateToken(string token)
    {
      var tokenHandler = new JwtSecurityTokenHandler();
      var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

      try
      {
        tokenHandler.ValidateToken(token, new TokenValidationParameters
        {
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(key),
          ValidateIssuer = true,
          ValidateAudience = true,
          ValidIssuer = _configuration["Jwt:Issuer"],
          ValidAudience = _configuration["Jwt:Audience"],
          ClockSkew = TimeSpan.Zero
        }, out SecurityToken validatedToken);

        return true;
      }
      catch
      {
        return false;
      }
    }
  }
}
