using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class PatientService : IPatientService
{
    private readonly IPatientRepository _basePatientRepository;

    public PatientService(
        IPatientRepository basePatientRepository)
    {
        _basePatientRepository = basePatientRepository;
    }
    
    public bool Validations(Patient patient)
    {
        if (_basePatientRepository.IsPatientExists(patient))
            throw new PatientExistesException(patient);

        return false;
    }

    public IList<Patient> GetAllPatientActive()
    {
        return _basePatientRepository.GetAllPatientActive();
    }

    public Patient GetPatientByUserId(string userId)
    {
        return _basePatientRepository.GetPatientByUserId(userId);
    }
}