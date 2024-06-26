﻿namespace DoctorAppointmentBooking.Presentation.BlazorWebApp.Configuration;

public class BaseUrlConfiguration
{
    public const string CONFIG_NAME = "baseUrls";
    public string BlobEndPoint { get;set; }
    public string ApiBase { get; set; }
    public string WebBase { get; set; }
    public string TokenAccess { get; set; }
}