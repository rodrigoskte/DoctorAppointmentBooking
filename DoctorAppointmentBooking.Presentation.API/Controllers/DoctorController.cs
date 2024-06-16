using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.Presentation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]/")]
public class DoctorController : BaseController
{
    private readonly IBaseService<Doctor> _baseDoctorService;
    private readonly IDoctorService _doctorService;
    private readonly IScheduleService _scheduleService;
    private readonly IAuthService _authService;

    public DoctorController(
        IBaseService<Doctor> baseDoctorService,
        IDoctorService doctorService,
        IScheduleService scheduleService,
        IAuthService authService)
    {
        _baseDoctorService = baseDoctorService;
        _doctorService = doctorService;
        _scheduleService = scheduleService;
        _authService = authService;
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
    public IActionResult Get([FromRoute] int id)
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
                Email = doctorDto.Email,
                IsDeleted = doctorDto.IsDeleted,
                UserId = ""
            };

            try
            {
                _doctorService.Validations(doctor);
                _baseDoctorService.Add<DoctorValidator>(doctor);
                _authService.CreateDoctorUser(doctor, doctorDto.Email);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return doctorDto;
        });
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(
        [FromRoute] int id,
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
                Email = doctorDto.Email,
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