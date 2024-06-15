using Microsoft.AspNetCore.Identity;

namespace DoctorAppointmentBooking.Domain.Interfaces
{
    public interface IUserManagerService
    {
        Task<IdentityResult> CreateAsync(IdentityUser user, string password);

        Task SignInAsync(IdentityUser user, bool isPersistent);

        Task<IdentityResult> SignInAsync(IdentityUser user, string role);

        Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role);

        Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure);

        Task<IdentityUser?> FindByIdAsync(string userId);

        Task<IdentityResult> UpdateAsync(IdentityUser user);

        Task<IdentityResult> CreateAddRoleAsync(
            IdentityUser user,
            string password,
            string role);

        Task<IdentityResult> UpdatePasswordAsync(
            IdentityUser user,
            string oldPassword,
            string newPassword);
       
    }
}
