using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        [Required]
        [Description("Specialty's description")]
        public string Description { get; set; }
        
        [JsonIgnore]
        public IList<DoctorSpecialty> DoctorSpecialties { get; private set; } = new List<DoctorSpecialty>();
    }
}
