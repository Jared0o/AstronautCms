using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AstronautEcommerce.Modules.Users.Core.Models;
using AstronautEcommerce.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AstronautEcommerce.Modules.Users.Infrastructure.Services;

public class TokenGenerator : ITokenGenerator
{
    private readonly UserManager<User> _userManager;
    private readonly IConfiguration _configuration;

    public TokenGenerator(UserManager<User> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }
    
    
    public async Task<string> GenerateTokenAsync(User user, CancellationToken ct = default)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        
        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));
        
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var expireMinutes = _configuration.GetValue<double>("Jwt:ExpireMinutes");
        
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: creds
        );
        
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}