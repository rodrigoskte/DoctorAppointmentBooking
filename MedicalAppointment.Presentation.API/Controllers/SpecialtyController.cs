using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class SpecialtyController : BaseController
{
    private readonly IBaseService<Specialty> _baseSpecialtyService;

    public SpecialtyController(IBaseService<Specialty> baseSpecialtyService)
    {
        _baseSpecialtyService = baseSpecialtyService;
    }
    
    [HttpPost]
    [Route("v1/specialty")]
    public IActionResult Create([FromBody] SpecialtyDto specialtyDto)
    {
        if(specialtyDto == null)
            return NotFound();

        return Execute(() =>
        {
            var specialty = new Specialty
            {
                Description = specialtyDto.Description,
                IsDeleted = specialtyDto.IsDeleted
            };
            _baseSpecialtyService.Add<SpecialtyValidator>(specialty);
            return Ok(specialtyDto);
        });
    }
    
    [HttpGet]
    [Route("v1/specialty")]
    public IActionResult Get()
    {
        return Execute(() => _baseSpecialtyService.Get());
    }
    
    [HttpGet]
    [Route("v1/specialty/{id}")]
    public IActionResult Get(int id)
    {
        if (id == 0)
            return NotFound();

        return Execute(() => _baseSpecialtyService.GetById(id));
    }
    
    [HttpPut]
    [Route("v1/specialty")]
    public IActionResult Update([FromBody] SpecialtyDto specialtyDto)
    {
        if (specialtyDto == null)
            return NotFound();

        return Execute(() =>
        {
            var specialty = new Specialty
            {
                Description = specialtyDto.Description,
                IsDeleted = specialtyDto.IsDeleted
            };
            _baseSpecialtyService.Update<SpecialtyValidator>(specialty);
            return Ok(specialtyDto);
        });
    }

    [HttpDelete]
    [Route("v1/specialty/{id}")]
    public IActionResult Delete(int id)
    {
        if (id == 0)
            return NotFound();

        Execute(() =>
        {
            _baseSpecialtyService.Delete(id);
            return true;
        });

        return new NoContentResult();
    }
}