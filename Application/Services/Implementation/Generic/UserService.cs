using Application.Dtos.AppUserDtos;
using Application.Services.Interfaces.Generic;
using Domain;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IConfiguration _config;

    public UserService(UserManager<AppUser> userManager, IConfiguration config)
    {
        _userManager = userManager;
        _config = config;
    }

    // Register
    public async Task<AuthResultDto> RegisterAsync(RegisterDto dto)
    {
        if (await _userManager.FindByEmailAsync(dto.Email) != null)
            return new AuthResultDto { Success = false, Error = "Email already taken" };

        var user = new AppUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            Fullname = dto.FullName
        };

        var result = await _userManager.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return new AuthResultDto
            {
                Success = false,
                Error = string.Join("; ", result.Errors.Select(e => e.Description))
            };

        await _userManager.AddToRoleAsync(user, "User");
        var roles = await _userManager.GetRolesAsync(user);

        var userDto = new UserDto
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            Role = roles.FirstOrDefault()
        };

        return new AuthResultDto
        {
            Success = true,
            User = userDto
        };
    }

    // Login
    public async Task<AuthResultDto> LoginAsync(LoginDto dto)
    {
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user == null) return new AuthResultDto { Success = false, Error = "Invalid credentials" };

        var valid = await _userManager.CheckPasswordAsync(user, dto.Password);
        if (!valid) return new AuthResultDto { Success = false, Error = "Invalid credentials" };

        var roles = await _userManager.GetRolesAsync(user);

        var token = GenerateToken(user, roles.FirstOrDefault() ?? "User");
        var userDto = new UserDto
        {
            Id = user.Id,
            Fullname = user.Fullname,
            Email = user.Email,
            Role = roles.FirstOrDefault()
        };

        return new AuthResultDto
        {
            Success = true,
            User = userDto,
            Token = token
        };
    }

    // توليد JWT
    private string GenerateToken(AppUser user, string role)
    {
        var jwt = _config.GetSection("Jwt");
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email ?? ""),
            new Claim("id", user.Id.ToString()),
            new Claim("fullname", user.Fullname ?? ""),
            new Claim(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"],
            audience: jwt["Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(int.Parse(jwt["DurationInMinutes"] ?? "60")),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
