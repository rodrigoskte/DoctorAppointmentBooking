using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Schedule : BaseEntity
    {
        [Required]
        public int DoctorId { get; set; }

        [Required]
        public int PatientId { get; set; }

        [Required]
        [Description("Date time schedule")]
        public DateTime DateTimeSchedule { get; set; }
        
        [Description("Patient's name")]
        [JsonIgnore]
        public Patient Patient { get; private set; }
        
        [Description("Doctor's name")]
        [JsonIgnore]
        public Doctor Doctor { get; private set; }
    }
}
