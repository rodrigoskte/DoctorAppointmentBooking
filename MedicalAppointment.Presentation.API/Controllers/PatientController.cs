using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]/")]
    public class PatientController : BaseController
    {
        private readonly IBaseService<Patient> _basePatientService;
        private readonly IPatientService _patientService;
        private readonly IScheduleService _scheduleService;

        public PatientController(
            IBaseService<Patient> baseUserService,
            IPatientService patientService,
            IScheduleService scheduleService)
        {
            _basePatientService = baseUserService;
            _patientService = patientService;
            _scheduleService = scheduleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Execute(() => _patientService.GetAllPatientActive());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult Get([FromRoute]int id)
        {
            if (id <= 0)
                return NotFound();

            return Execute(() => _basePatientService.GetById(id));
        }
        
        [HttpGet]
        [Route("GetAllPatient")]
        public IActionResult GetAllPatient()
        {
            return Execute(() => _basePatientService.Get());
        }
        
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
                    IsDeleted = patientDto.IsDeleted
                };
                
                _patientService.Validations(patient);
                
                _basePatientService.Add<PatientValidator>(patient);
                return patientDto;
            });
        }

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
                    IsDeleted = patientDto.IsDeleted
                };
                
                _patientService.Validations(patient);
                
                _basePatientService.Update<PatientValidator>(patient);
                return patientDto;
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
                _scheduleService.IsPatientActiveSchedule(id);
                
                _basePatientService.Delete(id);
                return true;
            });
        }
    }
}
