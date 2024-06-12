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
public class SpecialtyController : BaseController
{
    private readonly IBaseService<Specialty> _baseSpecialtyService;
    private readonly ISpecialtyService _specialtyService;

    public SpecialtyController(
        IBaseService<Specialty> baseSpecialtyService,
        ISpecialtyService specialtyService)
    {
        _baseSpecialtyService = baseSpecialtyService;
        _specialtyService = specialtyService;
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    public IActionResult Get()
    {
        return Execute(() => _specialtyService.GetAllSpecialtyActive());
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _baseSpecialtyService.GetById(id));
    }
    
    [Authorize(Roles = "Admin, Doctor")]
    [HttpGet]
    [Route("GetAllSpecialty")]
    public IActionResult GetAllSpecialty()
    {
        return Execute(() => _baseSpecialtyService.Get());
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPost]
    public IActionResult Create([FromBody] SpecialtyDto specialtyDto)
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
            
            _specialtyService.Validations(specialty);
            
            _baseSpecialtyService.Add<SpecialtyValidator>(specialty);
            return specialtyDto;
        });
    }

    [Authorize(Roles = "Admin, Doctor")]
    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update(
        [FromRoute] int id,
        [FromBody] SpecialtyDto specialtyDto)
    {
        if (id <= 0 || specialtyDto == null)
            return NotFound();

        return Execute(() =>
        {
            var specialty = new Specialty
            {
                Id = id,
                Description = specialtyDto.Description,
                IsDeleted = specialtyDto.IsDeleted
            };
            
            _baseSpecialtyService.Update<SpecialtyValidator>(specialty);
            return specialtyDto;
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
            _baseSpecialtyService.Delete(id);
            return true;
        });
    }
}