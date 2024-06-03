using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class DoctorController: BaseController
{
    private readonly IBaseService<Doctor> _baseDoctorService;
    private readonly IDoctorService _doctorService;

    public DoctorController(
        IBaseService<Doctor> baseDoctorService, 
        IDoctorService doctorService)
    {
        _baseDoctorService = baseDoctorService;
        _doctorService = doctorService;
    }
    
    [HttpGet]
    [Route("v1/doctor")]
    public IActionResult Get()
    {
        return Execute(() => _baseDoctorService.Get());
    }
    
    [HttpGet]
    [Route("v1/doctor/{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _baseDoctorService.GetById(id));
    }
    
    [HttpPost]
    [Route("v1/doctor")]
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
                DoctorSpecialties = doctorDto.DoctorSpecialties.Select(ds => new DoctorSpecialty
                {
                    SpecialtyId = ds.SpecialtyId,
                    DoctorId = doctorDto.Id,
                }).ToList()
            };
            _doctorService.AddDoctor(doctor);
            return doctorDto;
        });
    }
    
    [HttpPut]
    [Route("v1/doctor/{id:int}")]
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
                Id = doctorDto.Id,
                Name = doctorDto.Name,
                Code = doctorDto.Code,
                IsDeleted = doctorDto.IsDeleted,
                DoctorSpecialties = doctorDto.DoctorSpecialties.Select(ds => new DoctorSpecialty
                {
                    SpecialtyId = ds.SpecialtyId,
                }).ToList()
            };
            _baseDoctorService.Update<DoctorValidator>(doctor);
            return doctorDto;
        });
    }

    [HttpDelete]
    [Route("v1/doctor/{id:int}")]
    public IActionResult Delete([FromRoute] int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() =>
        {
            _baseDoctorService.Delete(id);
            return true;
        });
    }
}