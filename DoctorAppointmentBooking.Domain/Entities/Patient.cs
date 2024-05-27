using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Patient : BaseEntity
    {
        [Required]
        [Description("Patient's name")]
        public string Name { get; private set; }
        [Description("Patient's email")]
        public string Email { get; private set; }
        public bool IsDeleted { get; private set; }

        public IList<Schedule> Schedules { get; private set; }
    }
}
