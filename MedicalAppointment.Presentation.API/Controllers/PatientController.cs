using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MedicalAppointment.Presentation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]/")]
    public class PatientController : ControllerBase
    {
        private readonly IBaseService<Patient> _basePatientService;

        public PatientController(IBaseService<Patient> baseUserService)
        {
            _basePatientService = baseUserService;
        }

        private IActionResult Execute(Func<object> func)
        {
            try
            {
                var result = func();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost]
        [Route("v1/patient")]
        public IActionResult Create([FromBody] Patient patient)
        {
            if(patient == null)
                return NotFound();

            return Execute(() => _basePatientService.Add<PatientValidator>(patient).Id);
        }

        [HttpPut]
        [Route("v1/patient")]
        public IActionResult Update([FromBody] Patient patient)
        {
            if (patient == null)
                return NotFound();

            return Execute(() => _basePatientService.Update<PatientValidator>(patient));
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
