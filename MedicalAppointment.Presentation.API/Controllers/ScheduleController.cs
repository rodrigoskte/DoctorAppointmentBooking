using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers;

[ApiController]
[Route("api/v1/[controller]/")]
public class ScheduleController : BaseController
{
    private readonly IBaseService<Schedule> _baseScheduleService;
    private readonly IScheduleService _scheduleService;

    public ScheduleController(
        IBaseService<Schedule> baseScheduleService,
        IScheduleService scheduleService)
    {
        _baseScheduleService = baseScheduleService;
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public IActionResult Get()
    {
        return Execute(() => _scheduleService.GetAllWithDetails());
    }

    [HttpGet]
    [Route("{id:int}")]
    public IActionResult Get([FromRoute]int id)
    {
        if (id <= 0)
            return NotFound();

        return Execute(() => _scheduleService.GetAllWithDetailsId(id));
    }
    
    [HttpGet]
    [Route("GetAllSchedules")]
    public IActionResult GetAllSchedules()
    {
        return Execute(() => _baseScheduleService.Get());
    }
    
    [HttpPost]
    public IActionResult Create([FromBody] ScheduleDto scheduleDto)
    {
        if (scheduleDto == null)
            return NotFound();

        return Execute(() =>
        {
            var schedule = new Schedule
            {
                DoctorId = scheduleDto.DoctorId,
                PatientId = scheduleDto.PatientId,
                DateTimeSchedule = scheduleDto.DateTimeSchedule,
                IsDeleted = scheduleDto.IsDeleted
            };

            _scheduleService.Validations(schedule);
            
            _baseScheduleService.Add<ScheduleValidator>(schedule);
            return scheduleDto;
        });
    }

    [HttpPut]
    [Route("{id:int}")]
    public IActionResult Update([FromRoute]int id,
        [FromBody] ScheduleDto scheduleDto)
    {
        if (id <= 0 || scheduleDto == null)
            return NotFound();

        return Execute(() =>
        {
            var schedule = new Schedule
            {
                Id = id,
                DoctorId = scheduleDto.DoctorId,
                PatientId = scheduleDto.PatientId,
                DateTimeSchedule = scheduleDto.DateTimeSchedule,
                IsDeleted = scheduleDto.IsDeleted
            };
            
            _scheduleService.Validations(schedule);
            _baseScheduleService.Update<ScheduleValidator>(schedule);
            return scheduleDto;
        });
    }

    [HttpDelete]
    [Route("{id:int}")]
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

    [HttpGet]
    [Route("GetPatientSchedule/{patientId:int}")]
    public IActionResult GetPatientSchedule(
        [FromRoute]int patientId)
    {
        if (patientId <= 0)
            return NotFound();

        return Execute(() => _scheduleService.GetAllSchedulePatientId(patientId));
    }
    
    [HttpGet]
    [Route("GetDoctorSchedule/{doctorId:int}")]
    public IActionResult GetDoctorSchedule(
        [FromRoute]int doctorId)
    {
        if (doctorId <= 0)
            return NotFound();

        return Execute(() => _scheduleService.GetAllScheduleDoctorId(doctorId));
    }

    [HttpPut]
    [Route("CancelSchedulePatient/{patientId:int}")]
    public IActionResult CancelSchedulePatient(
        [FromRoute]int patientId)
    {
        if (patientId <= 0)
            return NotFound();
        
        return Execute(() =>
        {
            var schedule = new Schedule
            {
                PatientId = patientId,
                IsDeleted = true
            };
            
            _scheduleService.CancelSchedulePatient(patientId);
            return patientId;
        });
    }
    
    [HttpPut]
    [Route("CancelScheduleDoctor/{doctorId:int}")]
    public IActionResult CancelScheduleDoctor(
        [FromRoute]int doctorId)
    {
        if (doctorId <= 0)
            return NotFound();
        
        return Execute(() =>
        {
            var schedule = new Schedule
            {
                DoctorId = doctorId,
                IsDeleted = true
            };
            
            _scheduleService.CancelScheduleDoctor(doctorId);
            return doctorId;
        });
    }
}