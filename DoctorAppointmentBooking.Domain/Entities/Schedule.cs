using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        [Required]
        public bool IsDeleted { get; private set; }

        [Required]
        [Description("Doctor's name")]
        public Doctor Doctor { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [Description("Patient's name")]
        public Patient Patient { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [Description("Date time schedule")]
        public DateTime DateTimeSchedule { get; set; }
    }
}
