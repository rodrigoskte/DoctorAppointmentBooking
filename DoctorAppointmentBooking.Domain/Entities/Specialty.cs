using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Specialty : BaseEntity
    {
        [Required]
        [Description("Specialty's description")]
        public string Description { get; set; }
        
        public IList<DoctorSpecialty> DoctorSpecialties { get; private set; } = new List<DoctorSpecialty>();
    }
}
