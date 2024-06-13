using DoctorAppointmentBooking.Application.DTOs;
using DoctorAppointmentBooking.Application.Validators;
using DoctorAppointmentBooking.Domain.Entities;
using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoctorAppointmentBooking.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly JwtSettings _jwtSettings;
        private readonly IUserManagerService _userManagerService;
        private readonly IBaseService<Patient> _basePatientService;
        private readonly IBaseService<Doctor> _baseDoctorService;

        public AuthService(
            UserManager<IdentityUser> userManager,
            IOptions<JwtSettings> jwtSettings,
            IUserManagerService userManagerService,
            IBaseService<Patient> basePatientService,
            IBaseService<Doctor> baseDoctorService)
        {
            _userManager = userManager;
            _jwtSettings = jwtSettings.Value;
            _userManagerService = userManagerService;
            _basePatientService = basePatientService;
            _baseDoctorService = baseDoctorService;
        }

        public async Task<string> GenerateJwtToken(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, user.UserName),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            foreach (var role in roles)
                claims.Add(new Claim(ClaimTypes.Role, role));

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtSettings.Segredo);

            var token = tokenHandler.CreateToken(new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Issuer = _jwtSettings.Emissor,
                Audience = _jwtSettings.Audiencia,
                Expires = DateTime.UtcNow.AddHours(_jwtSettings.ExpiracaoHoras),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            });

            var encoded = tokenHandler.WriteToken(token);
            return encoded;
        }

        public void CreateDoctorUser(Doctor doctor, string email)
        {
            var user = new IdentityUser
            {
                Email = email
            };

            var result = _userManagerService.CreateAsync(user, email);

            if (result.Result.Succeeded)
            {
                doctor.UserId = user.Id;
                _baseDoctorService.Update<DoctorValidator>(doctor);
            }
            else
            {
                throw new Exception($"Failed to create doctor: {result.Result.Errors}");
            }
        }

        public void CreatePatientUser(Patient patient, string email)
        {
            var user = new IdentityUser
            {
                Email = email
            };

            var result = _userManagerService.CreateAsync(user, email);

            if (result.Result.Succeeded)
            {
                patient.UserId = user.Id;
                _basePatientService.Update<PatientValidator>(patient);
            }
            else
            {
                throw new Exception($"Failed to create patient: {result.Result.Errors}");
            }
        }
    }
}