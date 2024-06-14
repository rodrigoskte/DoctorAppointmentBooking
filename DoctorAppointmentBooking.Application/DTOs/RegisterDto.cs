using System.ComponentModel.DataAnnotations;

namespace DoctorAppointmentBooking.Application.DTOs;

public class RegisterDto
{
    [Required]
    public string Email { get; set; }

    [StringLength(100, MinimumLength = 6, ErrorMessage = "The Password must be at least 6 characters long.")]
    public string Password { get; set; }

    [Required]
    public string Role { get; set; }
}