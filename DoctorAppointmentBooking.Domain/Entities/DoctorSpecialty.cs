using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class DoctorSpecialty : BaseEntity
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public Doctor Doctor { get; set; }

        [Required]
        public int SpecialtyId { get; set; }

        [Required]
        public Specialty Specialty { get; set; }

    }
}
