using System.Text;
using api_rest_cs.Data;
using api_rest_cs.DTOs;
using api_rest_cs.Models;
using api_rest_cs.Services;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;

namespace api_rest_cs.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(AppDbContext context, JwtService jwtService) : ControllerBase
{
    private readonly AppDbContext _context = context;
    private readonly JwtService _jwtService = jwtService;

    [HttpPost("register")]
    public async Task<ActionResult> Register(RegisterDto dto)
    {
        var passwordHash = HashPassword(dto.Password);
        var user = new User { Username = dto.Username, PasswordHash = passwordHash };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok("Usuario registrado correctamente.");
    }

    [HttpPost("login")]
    public ActionResult Login(LoginDto dto)
    {
        var user = _context.Users.SingleOrDefault(u => u.Username == dto.Username);
        if (user == null || !VerifyPassword(dto.Password, user.PasswordHash))
        {
            return Unauthorized("Credenciales incorrectas.");
        }

        var token = _jwtService.GenerateToken(user.Id.ToString(), user.Username);
        return Ok(new { Token = token });
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        // var hash = sha256.ComputeHash(bytes);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }

    private static bool VerifyPassword(string password, string passwordHash)
    {
        var hash = HashPassword(password);
        return hash == passwordHash;
    }
}
