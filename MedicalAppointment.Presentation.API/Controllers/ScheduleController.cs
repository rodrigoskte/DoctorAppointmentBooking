using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[ApiController]
[Route("api/[controller]/")]
public class ScheduleController : BaseController
{
    private readonly IBaseService<Schedule> _baseScheduleService;

    public ScheduleController(IBaseService<Schedule> baseScheduleService)
    {
        _baseScheduleService = baseScheduleService;
    }

    [HttpGet]
    [Route("v1/schedule")]
    public IActionResult Get()
    {
        return Execute(() => _baseScheduleService.Get());
    }

    [HttpGet]
    [Route("v1/schedule/{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _baseScheduleService.GetById(id));
    }
    
    [HttpPost]
    [Route("v1/schedule")]
    public IActionResult Create([FromBody] ScheduleDto scheduleDto)
    {
        if (scheduleDto == null)
            return NotFound();

        return Execute(() =>
        {
            var specialty = new Schedule
            {
                DoctorId = scheduleDto.DoctorId,
                PatientId = scheduleDto.PatientId,
                DateTimeSchedule = scheduleDto.DateTimeSchedule,
                IsDeleted = scheduleDto.IsDeleted
            };
            _baseScheduleService.Add<ScheduleValidator>(specialty);
            return scheduleDto;
        });
    }

    [HttpPut]
    [Route("v1/schedule/{id:int}")]
    public IActionResult Update([FromRoute]int id,
        [FromBody] ScheduleDto scheduleDto)
    {
        if (id <= 0 || scheduleDto == null)
            return NotFound();

        return Execute(() =>
        {
            var specialty = new Schedule
            {
                Id = id,
                DoctorId = scheduleDto.DoctorId,
                PatientId = scheduleDto.PatientId,
                DateTimeSchedule = scheduleDto.DateTimeSchedule,
                IsDeleted = scheduleDto.IsDeleted
            };
            _baseScheduleService.Add<ScheduleValidator>(specialty);
            return scheduleDto;
        });
    }

    [HttpDelete]
    [Route("v1/schedule/{id:int}")]
    public IActionResult Delete([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() =>
        {
            _baseScheduleService.Delete(id);
            return true;
        });
    }
}