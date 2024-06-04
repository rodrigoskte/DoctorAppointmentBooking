using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Domain.Interfaces;

public interface IDoctorSpecialtyService
{
    IList<DoctorSpecialty> GetAllWithDetails();
    
    IList<DoctorSpecialty> GetAllWithDetailsId(int id);

    bool Validations(
        int doctorId,
        int specialtyId);
}