﻿using System.ComponentModel;

namespace DoctorAppointmentBooking.Application.DTOs;

public class UpdateUserDto
{
    public string Email { get; set; }
    
    public string UserName { get; set; }
    
    public string Password { get; set; }
}