using BaRemake.Shared.DTOs;
using BaRemake.Shared.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BaRemake.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _users;
    private readonly SignInManager<ApplicationUser> _signIn;
    private readonly IConfiguration _config;

    public AuthController(UserManager<ApplicationUser> users,
        SignInManager<ApplicationUser> signIn, IConfiguration config)
    {
        _users = users;
        _signIn = signIn;
        _config = config;
    }

    /// <summary>Register a new customer account.</summary>
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto dto)
    {
        if (!ModelState.IsValid) return ValidationProblem(ModelState);

        var user = new ApplicationUser
        {
            UserName = dto.Email,
            Email = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Title = dto.Title,
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            EmailConfirmed = true // skip email verification for demo
        };

        var result = await _users.CreateAsync(user, dto.Password);
        if (!result.Succeeded)
            return BadRequest(result.Errors.Select(e => e.Description));

        await _users.AddToRoleAsync(user, "Customer");
        await _users.AddClaimsAsync(user, new[]
        {
            new Claim("FullName", user.FullName),
            new Claim("FirstName", user.FirstName),
            new Claim("LastName", user.LastName),
            new Claim("IsAdmin", "false")
        });

        return Ok(new { message = "Registration successful." });
    }

    /// <summary>Login and receive a JWT token.</summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _users.FindByEmailAsync(dto.Email);
        if (user == null) return Unauthorized("Invalid credentials.");

        var result = await _signIn.CheckPasswordSignInAsync(user, dto.Password, lockoutOnFailure: true);
        if (!result.Succeeded)
        {
            if (result.IsLockedOut) return Unauthorized("Account locked. Try again later.");
            return Unauthorized("Invalid credentials.");
        }

        var token = await GenerateJwtToken(user);
        user.LastLoginAt = DateTime.UtcNow;
        await _users.UpdateAsync(user);

        return Ok(new
        {
            token,
            expiry = DateTime.UtcNow.AddMinutes(
                _config.GetValue<int>("Jwt:ExpiryMinutes", 60)),
            userId = user.Id,
            email = user.Email,
            fullName = user.FullName,
            roles = await _users.GetRolesAsync(user)
        });
    }

    /// <summary>Get current user profile.</summary>
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _users.GetUserAsync(User);
        if (user == null) return NotFound();

        return Ok(new
        {
            user.Id,
            user.Email,
            user.FirstName,
            user.LastName,
            user.FullName,
            user.Title,
            user.PhoneNumber,
            user.DateOfBirth,
            user.PassportNumber,
            user.PassportCountry,
            user.PreferredSeatPreference,
            roles = await _users.GetRolesAsync(user)
        });
    }

    /// <summary>Update user profile.</summary>
    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDto dto)
    {
        var user = await _users.GetUserAsync(User);
        if (user == null) return NotFound();

        user.FirstName = dto.FirstName;
        user.LastName = dto.LastName;
        user.Title = dto.Title;
        user.PhoneNumber = dto.PhoneNumber;
        user.PassportNumber = dto.PassportNumber;
        user.PassportCountry = dto.PassportCountry;
        user.PreferredSeatPreference = dto.PreferredSeatPreference;

        var result = await _users.UpdateAsync(user);
        if (!result.Succeeded) return BadRequest(result.Errors);

        return Ok(new { message = "Profile updated." });
    }

    private async Task<string> GenerateJwtToken(ApplicationUser user)
    {
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var roles = await _users.GetRolesAsync(user);
        var userClaims = await _users.GetClaimsAsync(user);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(ClaimTypes.NameIdentifier, user.Id),
            new("FullName", user.FullName),
            new("FirstName", user.FirstName),
            new("LastName", user.LastName),
        };

        claims.AddRange(roles.Select(r => new Claim(ClaimTypes.Role, r)));
        claims.AddRange(userClaims);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiryMinutes", 60)),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}

public class UpdateProfileDto
{
    public string Title { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string? PhoneNumber { get; set; }
    public string? PassportNumber { get; set; }
    public string? PassportCountry { get; set; }
    public string? PreferredSeatPreference { get; set; }
}
