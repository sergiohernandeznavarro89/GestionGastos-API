using Application.Dto;
using Domain.Entities;
using Domain.Repositories.Command;
using Infrastructure.Repositories.Query;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static Domain.Configuration.Sql;

namespace Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IUserQueryRepository _userQueryRepository;
    private readonly IUnitOfWorkFactory _unitOfWorkFactory;
    private readonly IConfiguration _configuration;

    public AuthController(IUserQueryRepository userQueryRepository, IUnitOfWorkFactory unitOfWorkFactory, IConfiguration configuration)
    {
        _userQueryRepository = userQueryRepository;
        _unitOfWorkFactory = unitOfWorkFactory;
        _configuration = configuration;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var user = await _userQueryRepository.FindByEmail(request.Email);
        if (user == null)
            return Unauthorized("Usuario o contraseña incorrectos");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.UserPass))
            return Unauthorized("Usuario o contraseña incorrectos");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
        
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Email, user.UserEmail),
                new Claim(ClaimTypes.Name, user.UserName)
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            Issuer = _configuration["Jwt:Issuer"],
            Audience = _configuration["Jwt:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var response = new AuthResponse
        {
            Token = tokenHandler.WriteToken(token),
            User = new UserResponse
            {
                UserId = user.UserId,
                UserName = user.UserName,
                UserLastName = user.UserLastName,
                UserEmail = user.UserEmail
            }
        };

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var existingUser = await _userQueryRepository.FindByEmail(request.Email);
        if (existingUser != null)
            return BadRequest("El email ya está registrado");

        var newUser = new User
        {
            UserName = request.UserName,
            UserLastName = request.UserLastName,
            UserEmail = request.Email,
            UserPass = BCrypt.Net.BCrypt.HashPassword(request.Password)
        };

        var uowFactory = _unitOfWorkFactory.Create();
        try
        {
            var userCommandRepo = uowFactory.GetRepository<IUserCommandRepository>();
            var userId = await userCommandRepo.Add(newUser);
            
            if (userId <= 0)
                return StatusCode(500, "Error al crear el usuario");

            uowFactory.SaveChanges();
            return Ok(new { Message = "Usuario registrado con éxito", UserId = userId });
        }
        catch (Exception)
        {
            uowFactory.UndoChanges();
            return StatusCode(500, "Error de base de datos al crear el usuario");
        }
    }
}
