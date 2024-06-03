using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class DoctorSpecialtyController : BaseController
{
    private readonly IBaseService<DoctorSpecialty> _baseDoctorSpecialtyService;

    public DoctorSpecialtyController(IBaseService<DoctorSpecialty> baseDoctorSpecialtyService)
    {
        _baseDoctorSpecialtyService = baseDoctorSpecialtyService;
    }
    
    [HttpGet]
    [Route("v1/doctorspecialty")]
    public IActionResult Get()
    {
        return Execute(() => _baseDoctorSpecialtyService.Get());
    }
    
    [HttpGet]
    [Route("v1/doctorspecialty/{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _baseDoctorSpecialtyService.GetById(id));
    }
}