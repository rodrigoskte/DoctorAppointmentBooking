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
    
    [Description("Doctor's CRM code")]
    public string Code { get; set; }

    [Description("Status")] 
    public bool IsDeleted { get; set; } = false;
    
    [Required]
    [Description("Doctor's specialty")]
    public IList<DoctorSpecialtyDto> DoctorSpecialties { get; set; } = new List<DoctorSpecialtyDto>();
}