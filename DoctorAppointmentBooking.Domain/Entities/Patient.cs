using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Patient : BaseEntity
    {
        public string Name { get; private set; }
        public string Email { get; private set; }
        public bool IsDeleted { get; private set; }
    }
}
