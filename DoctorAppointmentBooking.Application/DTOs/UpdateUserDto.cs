using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Application.DTOs;

public class UpdateUserDto
{
    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    [DataType(DataType.Password)]
    public string OldPassword { get; set; }
    
    [DataType(DataType.Password)]
    public string NewPassword { get; set; }
}