using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Application.DTOs;

public class SpecialtyDto
{
    public string Description { get; set; }
    
    public bool IsDeleted { get; set; } = false;
    
    public SpecialtyDto()
    {
        IsDeleted = false;
    }
}