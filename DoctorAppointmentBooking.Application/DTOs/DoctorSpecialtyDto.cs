using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Application.DTOs;

public class DoctorSpecialtyDto
{
    [Required]
    [Description("Specialty's id")]  
    public int SpecialtyId { get; set; }
    
    [Required]
    [Description("Doctor's id")]  
    public int DoctorId { get; set; }
}