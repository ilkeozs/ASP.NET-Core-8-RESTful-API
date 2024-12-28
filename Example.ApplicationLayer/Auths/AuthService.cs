using Example.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Example.ApplicationLayer.Auths;

public class AuthService : IAuthService
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IConfiguration _configuration;

    public AuthService(UserManager<IdentityUser> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<string> AuthenticateAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByNameAsync(loginRequest.Username);
        if (user == null || !await _userManager.CheckPasswordAsync(user, loginRequest.Password))
        {
            return null;
        }

        var userRoles = await _userManager.GetRolesAsync(user);

        var authClaims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.Role, userRoles.FirstOrDefault()!)
        };

        var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

        var token = new JwtSecurityToken(
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Audience"],
            claims: authClaims,
            expires: DateTime.Now.AddMinutes(60),
            signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
        );
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
