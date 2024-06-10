namespace MedicalAppointment.Presentation.API.Interface;

public interface IAuthService
{
    Task<string> GenerateJwtToken(string email);
}