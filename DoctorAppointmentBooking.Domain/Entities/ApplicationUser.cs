sing Microsoft.AspNetCore.Identity;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }

}
