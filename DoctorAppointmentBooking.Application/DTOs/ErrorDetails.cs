using System.Text.Json;

namespace DoctorAppointmentBooking.Application.DTOs;

public class ErrorDetails
{
    public int StatusCode { get; set; }
    public string Message { get; set; }
        
    public static ErrorDetails FromJson(string json)
    {
        return JsonSerializer.Deserialize<ErrorDetails>(json);
    }

    public override string ToString()
    {
        return JsonSerializer.Serialize(this);
    }
}