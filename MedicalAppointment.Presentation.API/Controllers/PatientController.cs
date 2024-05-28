using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class PatientController : BaseController
    {
        private readonly IBaseService<Patient> _basePatientService;

        public PatientController(IBaseService<Patient> baseUserService)
        {
            _basePatientService = baseUserService;
        }

        [HttpPost]
        [Route("v1/patient")]
        public IActionResult Create([FromBody] PatientDto patientDto)
        {
            if(patientDto == null)
                return NotFound();

            return Execute(() =>
            {
                var specialty = new Patient
                {
                    Name = patientDto.Name,
                    Email = patientDto.Email,
                    IsDeleted = patientDto.IsDeleted
                };
                _basePatientService.Add<PatientValidator>(specialty);
                return Ok(patientDto);
            });
        }

        [HttpPut]
        [Route("v1/patient")]
        public IActionResult Update([FromBody] PatientDto patientDto)
        {
            if (patientDto == null)
                return NotFound();

            return Execute(() =>
            {
                var specialty = new Patient
                {
                    Name = patientDto.Name,
                    Email = patientDto.Email,
                    IsDeleted = patientDto.IsDeleted
                };
                _basePatientService.Add<PatientValidator>(specialty);
                return Ok(patientDto);
            });
        }

        [HttpDelete]
        [Route("v1/patient/{id}")]
        public IActionResult Delete(int id)
        {
            if (id == 0)
                return NotFound();

            Execute(() =>
            {
                _basePatientService.Delete(id);
                return true;
            });

            return new NoContentResult();
        }

        [HttpGet]
        [Route("v1/patient")]
        public IActionResult Get()
        {
            return Execute(() => _basePatientService.Get());
        }

        [HttpGet]
        [Route("v1/patient/{id}")]
        public IActionResult Get(int id)
        {
            if (id == 0)
                return NotFound();

            return Execute(() => _basePatientService.GetById(id));
        }

    }
}
