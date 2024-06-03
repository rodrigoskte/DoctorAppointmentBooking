using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Application.DTOs;

public class ScheduleDto
{
    [Required]
    public int DoctorId { get; set; }
    
    [Required]
    public int PatientId { get; set; }
    
    [Required]
    [Description("Date time schedule")]
    public DateTime DateTimeSchedule { get; set; }
    
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    
    public ScheduleDto()
    {
        IsDeleted = false;
    }
}