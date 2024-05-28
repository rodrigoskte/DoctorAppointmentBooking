using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
    
    public bool IsDeleted { get; set; } = false;
}