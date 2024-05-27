namespace DoctorAppointmentBooking.Domain.Entities
{
    public class DoctorSpecialty : BaseEntity
    {
        public int DoctorId { get; set; }

        public Doctor Doctor { get; set; }

        public int SpecialtyId { get; set; }

        public Specialty Specialty { get; set; }
    }
}