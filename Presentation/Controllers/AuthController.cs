using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Shop.Core.Domain.Interfaces;
using Shop.Application.DTOs;
using Shop.Core.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shop.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;   

    public AuthController(IUserRepository userRepository, IConfiguration configuration)  
    {
        _userRepository = userRepository;
        _configuration = configuration;               
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] UserDto dto)
    {
        var user = new User
        {
            Username = dto.Username,
            Email = dto.Email,
            PasswordHash = dto.Password, 
            Role = "User"  
        };
        await _userRepository.AddAsync(user);
        return Ok(new { message = "User registered successfully" });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] UserDto dto)
    {
        var user = await _userRepository.GetByUsernameAsync(dto.Username);
        if (user == null) return Unauthorized();

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, user.Role ?? "User")
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? ""));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddHours(24),
            signingCredentials: credentials
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);

        return Ok(new
        {
            token = tokenString,
            username = user.Username,
            email = user.Email,
            role = user.Role
        });
    }
}