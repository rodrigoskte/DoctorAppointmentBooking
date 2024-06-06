using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class SpecialtyService: ISpecialtyService
{
    private readonly ISpecialtyRepository _specialtyRepository;

    public SpecialtyService(
        ISpecialtyRepository specialtyRepository)
    {
        _specialtyRepository = specialtyRepository;
    }

    public bool Validations(Specialty specialty)
    {
        if (_specialtyRepository.IsSpecialtyExists(specialty))
            throw new SpecialtyException(specialty);

        return false;
    }

    public IList<Specialty> GetAllSpecialtyActive()
    {
        return _specialtyRepository.GetAllSpecialtyActive();
    }
}