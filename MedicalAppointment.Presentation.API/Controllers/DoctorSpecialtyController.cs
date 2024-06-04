using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

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
    
    [HttpGet]
    public IActionResult Get()
    {
        return Execute(() => _doctorSpecialtyService.GetAllWithDetails());
    }
    
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _doctorSpecialtyService.GetAllWithDetailsId(id));
    }
    
    [HttpGet]
    [Route("GetAllDoctorSpecialty")]
    public IActionResult GetAllDoctorSpecialty()
    {
        return Execute(() => _baseDoctorSpecialtyService.Get());
    }
    
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