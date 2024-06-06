namespace DoctorAppointmentBooking.Application.Responses;

public class ApiResponse<T>
{
    public T Data { get; set; }
}