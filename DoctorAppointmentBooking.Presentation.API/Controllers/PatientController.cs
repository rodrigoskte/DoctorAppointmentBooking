﻿using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DoctorAppointmentBooking.Presentation.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class PatientController : BaseController
    {
        private readonly IBaseService<Patient> _basePatientService;
        private readonly IPatientService _patientService;
        private readonly IScheduleService _scheduleService;
        private readonly IAuthService _authService;

        public PatientController(
            IBaseService<Patient> baseUserService,
            IPatientService patientService,
            IScheduleService scheduleService,
            IAuthService authService)
        {
            _basePatientService = baseUserService;
            _patientService = patientService;
            _scheduleService = scheduleService;
            _authService = authService;
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _patientService.GetAllPatientActive());
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (id <= 0)
                return NotFound();

            return Execute(() => _basePatientService.GetById(id));
        }
        
        [Authorize(Roles = "Admin, Patient")]
        [HttpGet]
        [Route("GetAllPatient")]
        public IActionResult GetAllPatient()
        {
            return Execute(() => _basePatientService.Get());
        }
        
        [Authorize(Roles = "Admin, Patient")]
        [HttpPost]
        public IActionResult Create([FromBody] PatientDto patientDto)
        {
            if(patientDto == null)
                return NotFound();

            return Execute(() =>
            {
                var patient = new Patient
                {
                    Name = patientDto.Name,
                    Email = patientDto.Email,
                    IsDeleted = patientDto.IsDeleted,
                    UserId = ""
                };

                try
                {
                    _patientService.Validations(patient);
                    _basePatientService.Add<PatientValidator>(patient);
                    _authService.CreatePatientUser(patient, patientDto.Email);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

                return patientDto;
            });
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpPut]
        [Route("{id:int}")]
        public IActionResult Update(
            [FromRoute]int id,
            [FromBody] PatientDto patientDto)
        {
            if (id <= 0 || patientDto == null)
                return NotFound();

            return Execute(() =>
            {
                var patient = new Patient
                {
                    Id = id,
                    Name = patientDto.Name,
                    Email = patientDto.Email,
                    IsDeleted = patientDto.IsDeleted,
                    UserId = ""
                };
                
                _basePatientService.Update<PatientValidator>(patient);
                return patientDto;
            });
        }

        [Authorize(Roles = "Admin, Patient")]
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (id <= 0)
                return NotFound();

            return Execute(() =>
            {
                _scheduleService.IsPatientActiveSchedule(id);
                
                _basePatientService.Delete(id);
                return true;
            });
        }
    }
}
