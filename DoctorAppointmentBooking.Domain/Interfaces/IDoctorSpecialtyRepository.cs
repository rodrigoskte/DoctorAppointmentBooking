using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IDoctorSpecialtyRepository
{
    IList<DoctorSpecialty> GetAllWithDetails();
    
    IList<DoctorSpecialty> GetAllWithDetailsId(int id);

    bool IsDoctorSpecialtyExists(
        int doctorId,
        int specialtyId);
}