using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.ViewModels;
using DoctorAppointmentBooking.Infrastructure.Context;
using MedicalAppointment.Presentation.API.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/")]
public class AuthController : BaseController
{
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SqlDbContext _sqlDbContext;
    private readonly IAuthService _authService;
    private readonly IPasswordHasher<IdentityUser> _passwordHasher;

    public AuthController(
        SignInManager<IdentityUser> signInManager,
        UserManager<IdentityUser> userManager,
        SqlDbContext sqlDbContext,
        IAuthService authService,
        IPasswordHasher<IdentityUser> passwordHasher)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _sqlDbContext = sqlDbContext;
        _authService = authService;
        _passwordHasher = passwordHasher;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var user = new IdentityUser { UserName = model.Email, Email = model.Email, EmailConfirmed = true };
        var result = await _userManager.CreateAsync(user, model.Password);

        if (result.Succeeded)
        {
            await _signInManager.SignInAsync(user, false);
            await _userManager.AddToRoleAsync(user, model.Role);
            var token = await _authService.GenerateJwtToken(model.Email);
            return Ok(new ResultViewModel<object>(token, StatusCodes.Status200OK));
        }

        return BadRequest(
            new ResultViewModel<IEnumerable<IdentityError>>(result.Errors, StatusCodes.Status400BadRequest));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid) return BadRequest();

        var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, false, true);
        
        if (result.Succeeded)
        {
            var token = await _authService.GenerateJwtToken(model.Email);
            return Ok(new ResultViewModel<TokenResponseDto>(new TokenResponseDto { Token = token.ToString() },
                StatusCodes.Status200OK));
        }

        return Unauthorized(new ResultViewModel<string>("Usuario não autorizado", StatusCodes.Status401Unauthorized));
    }

    [HttpPost("Link-Doctor")]
    public async Task<IActionResult> LinkUserToDoctor([FromBody] LinkUserToDoctorDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null)
        {
            return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));
        }

        var doctor = _sqlDbContext.Doctors.FirstOrDefault(d => d.Id == dto.DoctorId);
        if (doctor == null)
        {
            return NotFound(new ResultViewModel<string>("Doctor not found", StatusCodes.Status404NotFound));
        }

        doctor.UserId = user.Id;
        _sqlDbContext.Doctors.Update(doctor);
        await _sqlDbContext.SaveChangesAsync();

        return Ok(new ResultViewModel<object>("User linked to Doctor successfully", StatusCodes.Status200OK));
    }

    [HttpPost("Link-Patient")]
    public async Task<IActionResult> LinkUserToPatient([FromBody] LinkUserToPatientDto dto)
    {
        var user = await _userManager.FindByIdAsync(dto.UserId);
        if (user == null)
        {
            return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));
        }

        var patient = _sqlDbContext.Patients.FirstOrDefault(d => d.Id == dto.PatientId);
        if (patient == null)
        {
            return NotFound(new ResultViewModel<string>("Patient not found", StatusCodes.Status404NotFound));
        }

        patient.UserId = user.Id;
        _sqlDbContext.Patients.Update(patient);
        await _sqlDbContext.SaveChangesAsync();

        return Ok(new ResultViewModel<object>("User linked to Doctor successfully", StatusCodes.Status200OK));
    }

    [Authorize]
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> FindUserById([FromRoute]string id)
    {
        if(string.IsNullOrEmpty(id))
            return BadRequest(new ResultViewModel<string>("ID is not valid", StatusCodes.Status400BadRequest));

        var user = await _userManager.FindByIdAsync(id);
        if (user != null)
        {
            return Ok(new ResultViewModel<IdentityUser>(user, StatusCodes.Status200OK));
        }
        return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));
    }

    [Authorize]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute]string id,
        [FromBody] UpdateUserDto model)
    {
        if (!ModelState.IsValid) return BadRequest(new ResultViewModel<string>("Model is not valid", StatusCodes.Status400BadRequest));

        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));

        if (!string.IsNullOrEmpty(model.Password))
            user.PasswordHash = _passwordHasher.HashPassword(user, model.Password);
        
        user.UserName = model.UserName;

        var result = await _userManager.UpdateAsync(user);
        if (result.Succeeded)
        {
            return Ok(new ResultViewModel<string>("User updated successfully", StatusCodes.Status200OK));
        }

        return BadRequest(new ResultViewModel<IEnumerable<IdentityError>>(result.Errors, StatusCodes.Status400BadRequest));
    }
}