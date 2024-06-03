using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Patient : BaseEntity
    {
        [Required]
        [Description("Patient's name")]
        public string Name { get; set; }
        [Description("Patient's email")]
        public string Email { get; set; }
        
        [JsonIgnore]
        public IList<Schedule> Schedules { get; private set; } = new List<Schedule>();
    }
}
