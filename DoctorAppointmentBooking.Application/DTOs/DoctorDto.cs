using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DoctorAppointmentBooking.Application.DTOs;

public class DoctorDto
{
    [JsonIgnore]
    public int Id { get; set; }
    
    [Required]
    [Description("Doctor's name")]   
    public string Name { get; set; }

    [Description("Doctor's email")]
    public string Email { get; set; }

    [Description("Status")] 
    [JsonIgnore]
    public bool IsDeleted { get; set; } = false;
    
    [Description("Doctor's specialty")]
    [JsonIgnore]
    public IList<DoctorSpecialtyDto> DoctorSpecialties { get; set; } = new List<DoctorSpecialtyDto>();
    
    public DoctorDto()
    {
        IsDeleted = false;
    }
}