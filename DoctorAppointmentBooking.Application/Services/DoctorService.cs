using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _doctorRepository;

    public DoctorService(IDoctorRepository doctorRepository)
    {
        _doctorRepository = doctorRepository;
    }

    public IList<Doctor> GetDoctorsWithSpecialties()
    {
        return _doctorRepository.GetDoctorsWithSpecialties();
    }

    public IList<Doctor> GetDoctorsWithSpecialtiesId(int id)
    {
        return _doctorRepository.GetDoctorsWithSpecialtiesId(id);
    }
}