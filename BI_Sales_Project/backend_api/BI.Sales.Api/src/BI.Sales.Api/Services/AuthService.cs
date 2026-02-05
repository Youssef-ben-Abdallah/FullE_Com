using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BI.Sales.Api.DTOs;
using BI.Sales.Api.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace BI.Sales.Api.Services;

public class AuthService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtOptions _jwtOptions;

    public AuthService(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<JwtOptions> jwtOptions)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _jwtOptions = jwtOptions.Value;
    }

    public async Task<AuthResponse?> AuthenticateAsync(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null)
        {
            return null;
        }

        var valid = await _userManager.CheckPasswordAsync(user, request.Password);
        if (!valid)
        {
            return null;
        }

        return await GenerateTokenAsync(user);
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest request)
    {
        var user = new ApplicationUser { UserName = request.Email, Email = request.Email };
        var result = await _userManager.CreateAsync(user, request.Password);
        if (result.Succeeded)
        {
            await _userManager.AddToRoleAsync(user, "User");
        }

        return result;
    }

    public async Task SeedAsync()
    {
        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await _roleManager.RoleExistsAsync(role))
            {
                await _roleManager.CreateAsync(new IdentityRole(role));
            }
        }

        await EnsureUserAsync("admin@local", "P@ssw0rd!", "Admin");
        await EnsureUserAsync("user@local", "P@ssw0rd!", "User");
    }

    private async Task EnsureUserAsync(string email, string password, string role)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            user = new ApplicationUser { UserName = email, Email = email };
            await _userManager.CreateAsync(user, password);
        }

        if (!await _userManager.IsInRoleAsync(user, role))
        {
            await _userManager.AddToRoleAsync(user, role);
        }
    }

    public async Task<AuthResponse> GenerateTokenAsync(ApplicationUser user)
    {
        var roles = await _userManager.GetRolesAsync(user);
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Name, user.Email ?? string.Empty)
        };

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var expires = DateTime.UtcNow.AddMinutes(_jwtOptions.ExpMinutes);

        var token = new JwtSecurityToken(
            issuer: _jwtOptions.Issuer,
            audience: _jwtOptions.Audience,
            claims: claims,
            expires: expires,
            signingCredentials: creds);

        return new AuthResponse(new JwtSecurityTokenHandler().WriteToken(token), expires);
    }
}
