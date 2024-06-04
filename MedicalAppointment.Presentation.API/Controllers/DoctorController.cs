using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[ApiController]
[Route("api/v1/[controller]/")]
public class DoctorController: BaseController
{
    private readonly IBaseService<Doctor> _baseDoctorService;
    private readonly IDoctorService _doctorService;
    private readonly IScheduleService _scheduleService;

    public DoctorController(
        IBaseService<Doctor> baseDoctorService, 
        IDoctorService doctorService,
        IScheduleService scheduleService)
    {
        _baseDoctorService = baseDoctorService;
        _doctorService = doctorService;
        _scheduleService = scheduleService;
    }
    
    [HttpGet]
    public IActionResult Get()
    {
        return Execute(() => _doctorService.GetAllDoctorsActive());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _doctorService.GetAllDoctorsActiveById(id));
    }
    
    [HttpGet]
    [Route("GetAllDoctor")]
    public IActionResult GetAllDoctor()
    {
        return Execute(() => _baseDoctorService.Get());
    }
    
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
                IsDeleted = doctorDto.IsDeleted
            };

            _doctorService.Validations(doctor);
            
            _baseDoctorService.Add<DoctorValidator>(doctor);
            return doctorDto;
        });
    }
    
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
                IsDeleted = doctorDto.IsDeleted
            };
            
            _doctorService.Validations(doctor);
            _baseDoctorService.Update<DoctorValidator>(doctor);
            return doctorDto;
        });
    }

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