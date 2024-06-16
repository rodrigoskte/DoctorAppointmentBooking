using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using DoctorAppointmentBooking.Domain.Entities;

namespace DoctorAppointmentBooking.Application.DTOs;

public class ScheduleDto
{
    public int Id { get; set; }
    
    [Required]
    public int DoctorId { get; set; }
    
    [Required]
    public int PatientId { get; set; }
    
    public string DoctorUserId { get; set; }
    
    public string PatientUserId { get; set; }
    
    [Required]
    [Description("Date time schedule")]
    public DateTime? DateTimeSchedule { get; set; }
    
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    
    public ScheduleDto()
    {
        IsDeleted = false;
    }
}