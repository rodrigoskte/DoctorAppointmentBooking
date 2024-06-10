using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[Authorize]
[ApiController]
[Route("api/v1/[controller]/")]
public class DoctorSpecialtyController : BaseController
{
    private readonly IBaseService<DoctorSpecialty> _baseDoctorSpecialtyService;
    private readonly IDoctorSpecialtyService _doctorSpecialtyService;

    public DoctorSpecialtyController(
        IBaseService<DoctorSpecialty> baseDoctorSpecialtyService,
        IDoctorSpecialtyService doctorSpecialtyService)
    {
        _baseDoctorSpecialtyService = baseDoctorSpecialtyService;
        _doctorSpecialtyService = doctorSpecialtyService;
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    public IActionResult Get()
    {
        return Execute(() => _doctorSpecialtyService.GetAllWithDetails());
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _doctorSpecialtyService.GetAllWithDetailsId(id));
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    [Route("GetAllDoctorSpecialty")]
    public IActionResult GetAllDoctorSpecialty()
    {
        return Execute(() => _baseDoctorSpecialtyService.Get());
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost]
    public IActionResult Create([FromBody] DoctorSpecialtyDto doctorSpecialtyDto)
    {
        if (doctorSpecialtyDto == null)
            return NotFound();

        return Execute(() =>
        {
            _doctorSpecialtyService.Validations(
                doctorSpecialtyDto.DoctorId,
                doctorSpecialtyDto.SpecialtyId);
            
            var doctorSpecialty = new DoctorSpecialty
            {
                DoctorId = doctorSpecialtyDto.DoctorId,
                SpecialtyId = doctorSpecialtyDto.SpecialtyId
            };
            _baseDoctorSpecialtyService.Add<DoctorSpecialtyValidator>(doctorSpecialty);
            return doctorSpecialty;
        });
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(
        [FromRoute]int id,
        [FromBody] DoctorSpecialtyDto doctorSpecialtyDto)
    {
        if (id <= 0 || doctorSpecialtyDto == null)
            return NotFound();

        return Execute(() =>
        {
            _doctorSpecialtyService.Validations(
                doctorSpecialtyDto.DoctorId,
                doctorSpecialtyDto.SpecialtyId);
            
            var doctorSpecialty = new DoctorSpecialty
            {
                Id = id,
                DoctorId = doctorSpecialtyDto.DoctorId,
                SpecialtyId = doctorSpecialtyDto.SpecialtyId
            };
            _baseDoctorSpecialtyService.Update<DoctorSpecialtyValidator>(doctorSpecialty);
            return doctorSpecialtyDto;
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
            _baseDoctorSpecialtyService.Delete(id);
            return true;
        });
    }
}