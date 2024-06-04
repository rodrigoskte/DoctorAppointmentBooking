using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Exceptions;
using DoctorAppointmentBooking.Domain.Interfaces;

namespace DoctorAppointmentBooking.Application.Services;

public class DoctorSpecialtyService : IDoctorSpecialtyService
{
    private readonly IDoctorSpecialtyRepository _doctorSpecialtyRepository;
    private readonly IBaseService<Specialty> _baseSpecialtyService;
    private readonly IBaseService<Doctor> _baseDoctorService;

    public DoctorSpecialtyService(
        IDoctorSpecialtyRepository doctorSpecialtyRepository,
        IBaseService<Specialty> baseSpecialtyService,
        IBaseService<Doctor> baseDoctorService)
    {
        _doctorSpecialtyRepository = doctorSpecialtyRepository;
        _baseSpecialtyService = baseSpecialtyService;
        _baseDoctorService = baseDoctorService;
    }
    
    public IList<DoctorSpecialty> GetAllWithDetails()
    {
        return _doctorSpecialtyRepository.GetAllWithDetails();
    }

    public IList<DoctorSpecialty> GetAllWithDetailsId(int id)
    {
        return _doctorSpecialtyRepository.GetAllWithDetailsId(id);
    }

    public bool Validations(
        int doctorId,
        int specialtyId)
    {
        if (_doctorSpecialtyRepository.IsDoctorSpecialtyExists(doctorId, specialtyId))
            throw new DoctorSpecialtyException(
                _baseDoctorService.GetById(doctorId)?.Name,
                _baseSpecialtyService.GetById(specialtyId)?.Description);

        return false;
    }
}