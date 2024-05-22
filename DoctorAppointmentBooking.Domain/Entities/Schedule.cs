using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        [Required]
        public bool IsDeleted { get; private set; }

        [Required]
        [JsonPropertyName("Doctor's name")]
        public Doctor Doctor { get; set; }

        [Required]
        public int DoctorId { get; set; }

        [Required]
        [JsonPropertyName("Patient's name")]
        public Patient Patient { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [JsonPropertyName("Date time schedule")]
        public DateTime DateTimeSchedule { get; set; }
    }
}
