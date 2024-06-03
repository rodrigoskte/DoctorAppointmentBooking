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

    [HttpGet]
    [Route("v1/specialty")]
    public IActionResult Get()
    {
        return Execute(() => _baseSpecialtyService.Get());
    }

    [HttpGet]
    [Route("v1/specialty/{id:int}")]
    public IActionResult Get([FromRoute] int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _baseSpecialtyService.GetById(id));
    }

    [HttpPost]
    [Route("v1/specialty")]
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
            _baseSpecialtyService.Add<SpecialtyValidator>(specialty);
            return specialtyDto;
        });
    }

    [HttpPut]
    [Route("v1/specialty/{id:int}")]
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

    [HttpDelete]
    [Route("v1/specialty/{id:int}")]
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