namespace DoctorAppointmentBooking.Application.ViewModels;

public class ResultViewModel<T>
{
    public T Data { get; private set; }
    public List<string> Errors { get; private set; } = new List<string>();
    public int StatusCode { get; private set; }

    public ResultViewModel(T data, int statusCode)
    {
        Data = data;
        StatusCode = statusCode;
    }

    public ResultViewModel(T data, List<string> errors, int statusCode)
    {
        Data = data;
        Errors = errors;
        StatusCode = statusCode;
    }

    public ResultViewModel(List<string> errors, int statusCode)
    {
        Errors = errors;
        StatusCode = statusCode;
    }

    public ResultViewModel(string error, int statusCode)
    {
        Errors.Add(error);
        StatusCode = statusCode;
    }
}