using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        [Required]
        [JsonPropertyName("Specialty's description")]
        public string Description { get; private set; }

        [Required]
        public bool IsDeleted { get; private set; }

        public IList<DoctorSpecialty> DoctorSpecialties { get; private set; }
    }
}
