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
public class ScheduleController : BaseController
{
    private readonly IBaseService<Schedule> _baseScheduleService;
    private readonly IScheduleService _scheduleService;
    private readonly IPatientService _patientService;
    private readonly IDoctorService _doctorService;

    public ScheduleController(
        IBaseService<Schedule> baseScheduleService,
        IScheduleService scheduleService,
        IPatientService patientService,
        IDoctorService doctorService)
    {
        _baseScheduleService = baseScheduleService;
        _scheduleService = scheduleService;
        _patientService = patientService;
        _doctorService = doctorService;
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
    [Route("GetPatientSchedule/{patientId}")]
    public IActionResult GetPatientSchedule(
        [FromRoute]string patientId)
    {
        if (string.IsNullOrEmpty(patientId))
            return NotFound();

        var patient = _patientService.GetPatientByUserId(patientId);
        if(patient == null)
            return NotFound();
        
        return Execute(() => _scheduleService.GetAllSchedulePatientId(patient.Id));
    }
    
    [HttpGet]
    [Route("GetDoctorSchedule/{doctorId}")]
    public IActionResult GetDoctorSchedule(
        [FromRoute]string doctorId)
    {
        if (string.IsNullOrEmpty(doctorId))
            return NotFound();

        var doctor = _doctorService.GetDoctorByUserId(doctorId);
        if(doctor == null)
            return NotFound();

        return Execute(() => _scheduleService.GetAllScheduleDoctorId(doctor.Id));
    }

    [HttpPut]
    [Route("CancelSchedulePatient/{patientId}")]
    public IActionResult CancelSchedulePatient(
        [FromRoute]string patientId)
    {
        if (string.IsNullOrEmpty(patientId))
            return NotFound();
        
        var patient = _patientService.GetPatientByUserId(patientId);
        if(patient == null)
            return NotFound();
        
        return Execute(() =>
        {
            var schedule = new Schedule
            {
                PatientId = patient.Id,
                IsDeleted = true
            };
            
            _scheduleService.CancelSchedulePatient(patient.Id);
            return patientId;
        });
    }
    
    [HttpPut]
    [Route("CancelScheduleDoctor/{doctorId}")]
    public IActionResult CancelScheduleDoctor(
        [FromRoute]string doctorId)
    {
        if (string.IsNullOrEmpty(doctorId))
            return NotFound();
        
        var doctor = _doctorService.GetDoctorByUserId(doctorId);
        if(doctor == null)
            return NotFound();
        
        return Execute(() =>
        {
            var schedule = new Schedule
            {
                DoctorId = doctor.Id,
                IsDeleted = true
            };
            
            _scheduleService.CancelScheduleDoctor(doctor.Id);
            return doctorId;
        });
    }
}