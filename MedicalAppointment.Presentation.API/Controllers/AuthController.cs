using DoctorAppointmentBooking.Application.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DoctorAppointmentBooking.Application.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace MedicalAppointment.Presentation.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/")]
public class AuthController : ControllerBase
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly JwtSettings _jwtSettings;

    public AuthController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        IOptions<JwtSettings> jwtSettings)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
    }
    
    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid) return BadRequest();
        
        var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true};
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, model.Role);
            var token = await GerarJwt(model.Email);
            return Ok(new ResultViewModel<object>(token, StatusCodes.Status200OK));
        }

        return BadRequest(new ResultViewModel<IEnumerable<IdentityError>>(result.Errors, StatusCodes.Status400BadRequest));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);

        if (result.Succeeded)
        {
            var token = await GerarJwt(model.Email);
            return Ok(new ResultViewModel<TokenResponseDto>(new TokenResponseDto { Token = token.ToString() }, StatusCodes.Status200OK));
        }

        return Unauthorized(new ResultViewModel<string>("Usuario não autorizado", StatusCodes.Status401Unauthorized));
    }

    private async Task<object?> GerarJwt(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        var roles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName)
        };
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }


        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

        var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Issuer = _jwtSettings.Emissor,
            Audience = _jwtSettings.Audiencia,
            Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        });

        var encoded = tokenHandler.WriteToken(token);
        return encoded;
    }
}