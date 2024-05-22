using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace DoctorAppointmentBooking.Domain.Entities
{
    public class Patient : BaseEntity
    {
        [Required]
        [JsonPropertyName("Patient's name")]
        public string Name { get; private set; }
        [JsonPropertyName("Patient's email")]
        public string Email { get; private set; }
        public bool IsDeleted { get; private set; }

        public IList<Schedule> Schedules { get; private set; }
    }
}
