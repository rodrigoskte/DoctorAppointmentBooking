using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Application.DTOs;

public class PatientDto
{
    [Required]
    [Description("Patient's name")]
    public string Name { get; set; }
    
    [Description("Patient's email")]
    public string Email { get; set; }
    
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    
    public PatientDto()
    {
        IsDeleted = false;
    }
}