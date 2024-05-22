using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        [Required]
        [JsonPropertyName("Doctor's name")]        
        public string Name { get; private set; }

        [JsonPropertyName("Doctor's CRM code")]
        public string Code { get; private set; }

        [Required]
        public bool IsDeleted { get; private set; }

        [Required]
        [JsonPropertyName("Doctor's specialty")]
        public IList<DoctorSpecialty> DoctorSpecialties { get; private set; }

        public IList<Schedule> Schedules { get; private set; }
    }
}
