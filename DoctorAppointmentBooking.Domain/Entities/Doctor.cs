using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Doctor : BaseEntity
    {
        [Required]
        [Description("Doctor's name")]        
        public string Name { get; set; }

        [Description("Doctor's CRM code")]
        public string Code { get; set; }

        [Required]
        [Description("Doctor's specialty")]
        public IList<DoctorSpecialty> DoctorSpecialties { get; set; }

        [Description("Doctor's schedule")]
        public IList<Schedule> Schedules { get; private set; } = new List<Schedule>();
    }
}
