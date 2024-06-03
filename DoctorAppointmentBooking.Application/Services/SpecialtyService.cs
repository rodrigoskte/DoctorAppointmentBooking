using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class SpecialtyService: ISpecialtyService
{
    private readonly ISpecialtyRepository _specialtyRepository;

    public SpecialtyService(ISpecialtyRepository specialtyRepository)
    {
        _specialtyRepository = specialtyRepository;
    }
}