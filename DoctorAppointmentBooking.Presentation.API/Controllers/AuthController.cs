using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.ViewModels;
using DoctorAppointmentBooking.Domain.Interfaces;
using DoctorAppointmentBooking.Infrastructure.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.Presentation.API.Controllers;

[AllowAnonymous]
[ApiController]
[Route("api/v1/[controller]/")]
public class AuthController : BaseController
{
    private readonly IUserManagerService _userManagerService;
    private readonly SqlDbContext _sqlDbContext;
    private readonly IAuthService _authService;

    public AuthController(
        IUserManagerService userManagerService,
        SqlDbContext sqlDbContext,
        IAuthService authService)
    {
        _userManagerService = userManagerService;
        _sqlDbContext = sqlDbContext;
        _authService = authService;
    }

    [HttpPost("Register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid) 
            return BadRequest();

        var user = new IdentityUser 
        { 
            UserName = model.Email, 
            Email = model.Email, 
            EmailConfirmed = true 
        };
        
        await _authService.CreateDoctorPatient(user, model.Email, model.Role);
        var token = await _authService.GenerateJwtToken(model.Email);
        if (!string.IsNullOrEmpty(token))
        {
            return Ok(new ResultViewModel<object>(token, StatusCodes.Status200OK));    
        }

        return BadRequest(
            new ResultViewModel<IEnumerable<IdentityError>>("Error, see the details", StatusCodes.Status400BadRequest));
    }

    [HttpPost("Login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid) 
            return BadRequest();

        var result = await _userManagerService.PasswordSignInAsync(model.Email, model.Password, false, true);
        
        if (result.Succeeded)
        {
            return Ok(new ResultViewModel<TokenResponseDto>(new TokenResponseDto { Token = await _authService.GenerateJwtToken(model.Email) },
                StatusCodes.Status200OK));
        }

        return Unauthorized(new ResultViewModel<string>("Usuario não autorizado", StatusCodes.Status401Unauthorized));
    }

    [HttpPost("Link-Doctor")]
    public async Task<IActionResult> LinkUserToDoctor([FromBody] LinkUserToDoctorDto dto)
    {
        var user = await _userManagerService.FindByIdAsync(dto.UserId);
        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));

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
        var user = await _userManagerService.FindByIdAsync(dto.UserId);
        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));

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

        var user = await _userManagerService.FindByIdAsync(id);
        if (user != null)
            return Ok(new ResultViewModel<IdentityUser>(user, StatusCodes.Status200OK));

        return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));
    }

    [Authorize]
    [HttpPut]
    [Route("{id}")]
    public async Task<IActionResult> UpdateUser(
        [FromRoute]string id,
        [FromBody] UpdateUserDto model)
    {
        if (!ModelState.IsValid) 
            return BadRequest(new ResultViewModel<string>("Model is not valid", StatusCodes.Status406NotAcceptable));

        var user = await _userManagerService.FindByIdAsync(id);
        if (user == null)
            return NotFound(new ResultViewModel<string>("User not found", StatusCodes.Status404NotFound));

        if (!string.IsNullOrEmpty(model.NewPassword))
        {
            if(string.IsNullOrEmpty(model.OldPassword))
                return BadRequest(new ResultViewModel<string>("Password is invalid", StatusCodes.Status400BadRequest));
            
            var changePassword = await _userManagerService.UpdatePasswordAsync(
                user,
                model.OldPassword,
                model.NewPassword);
        } 

        user.Email = model.Email;
        user.UserName = model.UserName;
        
        var result = await _userManagerService.UpdateAsync(user);
        if (result.Succeeded)
            return Ok(new ResultViewModel<string>("User updated successfully", StatusCodes.Status200OK));

        return BadRequest(new ResultViewModel<IEnumerable<IdentityError>>(result.Errors, StatusCodes.Status400BadRequest));
    }
}