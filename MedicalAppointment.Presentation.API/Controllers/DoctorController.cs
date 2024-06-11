using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]/")]
public class DoctorController: BaseController
{
    private readonly IBaseService<Doctor> _baseDoctorService;
    private readonly IDoctorService _doctorService;
    private readonly IScheduleService _scheduleService;
    private readonly UserManager<IdentityUser> _userManager;
    
    public DoctorController(
        IBaseService<Doctor> baseDoctorService, 
        IDoctorService doctorService,
        IScheduleService scheduleService,
        UserManager<IdentityUser> userManager)
    {
        _baseDoctorService = baseDoctorService;
        _doctorService = doctorService;
        _scheduleService = scheduleService;
        _userManager = userManager;
    }

    [Authorize(Roles = "Admin, Doctor, Patient")]
    [HttpGet]
    public IActionResult Get()
    {
        return Execute(() => _doctorService.GetAllDoctorsActive());
    }
    
    [Authorize(Roles = "Admin, Doctor, Patient")]
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _baseDoctorService.GetById(id));
    }

    //[Authorize(Roles = "Admin, Doctor, Patient")]
    [HttpGet]
    [Route("GetAllDoctor")]
    public IActionResult GetAllDoctor()
    {
        return Execute(() => _baseDoctorService.Get());
    }
    
    [Authorize(Roles = "Admin, Doctor, Patient")]
    [HttpPost]
    public IActionResult Create([FromBody] DoctorDto doctorDto)
    {
        if (doctorDto == null)
            return NotFound();

        return Execute(() =>
        {
            var doctor = new Doctor
            {
                Id = doctorDto.Id,
                Name = doctorDto.Name,
                Code = doctorDto.Code,
                IsDeleted = doctorDto.IsDeleted,
                UserId = ""
            };

            _doctorService.Validations(doctor);
            
            _baseDoctorService.Add<DoctorValidator>(doctor);
            
            var user = new IdentityUser
            {
                UserName = doctorDto.Code,
                Email = $"{doctorDto.Code}@doctor.com"
            };

            var result = _userManager.CreateAsync(user, "1234");

            if (result.Result.Succeeded)
            {
                doctor.UserId = user.Id;
                _baseDoctorService.Update<DoctorValidator>(doctor);
            }
            else
            {
                throw new Exception("Failed to create user");
            }
            
            return doctorDto;
        });
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(
        [FromRoute]int id,
        [FromBody] DoctorDto doctorDto)
    {
        if (id <= 0 || doctorDto == null)
            return NotFound();
        
        return Execute(() =>
        {
            var doctor = new Doctor
            {
                Id = id,
                Name = doctorDto.Name,
                Code = doctorDto.Code,
                IsDeleted = doctorDto.IsDeleted,
                UserId = ""
            };
            
            _baseDoctorService.Update<DoctorValidator>(doctor);
            return doctorDto;
        });
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpDelete]
    [Route("{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() =>
        {
            _scheduleService.IsDoctorActiveSchedule(id);
            
            _baseDoctorService.Delete(id);
            return true;
        });
    }
}