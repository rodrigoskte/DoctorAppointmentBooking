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
            return Ok(scheduleDto);
        });
    }

    [HttpPut]
    [Route("v1/schedule")]
    public IActionResult Update([FromBody] ScheduleDto scheduleDto)
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
            return Ok(scheduleDto);
        });
    }

    [HttpDelete]
    [Route("v1/schedule/{id}")]
    public IActionResult Delete(int id)
    {
        if (id == 0)
            return NotFound();

        Execute(() =>
        {
            _baseScheduleService.Delete(id);
            return true;
        });

        return new NoContentResult();
    }

    [HttpGet]
    [Route("v1/schedule")]
    public IActionResult Get()
    {
        return Execute(() => _baseScheduleService.Get());
    }

    [HttpGet]
    [Route("v1/schedule/{id}")]
    public IActionResult Get(int id)
    {
        if (id == 0)
            return NotFound();

        return Execute(() => _baseScheduleService.GetById(id));
    }
}