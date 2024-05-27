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
    public IActionResult Create([FromBody] Specialty specialty)
    {
        if(specialty == null)
            return NotFound();

        return Execute(() => _baseSpecialtyService.Add<SpecialtyValidator>(specialty).Id);
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
}