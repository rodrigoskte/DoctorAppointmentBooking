using DoctorAppointmentBooking.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace DoctorAppointmentBooking.Application.Services
{
    public class UserManagerService : IUserManagerService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;

        public UserManagerService(
            SignInManager<IdentityUser> signInManager,
            UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public async Task<IdentityResult> CreateAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        
        public async Task<IdentityResult> CreateAddRoleAsync(
            IdentityUser user,
            string password,
            string role)
        {
            var userCreated = await _userManager.CreateAsync(user, password);
            var userRole = await _userManager.AddToRoleAsync(user, role);
            return userCreated;
        }

        public async Task SignInAsync(IdentityUser user, bool isPersistent)
        {
            await _signInManager.SignInAsync(user, isPersistent);
        }

        public async Task<IdentityResult> SignInAsync(IdentityUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<IdentityResult> AddToRoleAsync(IdentityUser user, string role)
        {
            return await _userManager.AddToRoleAsync(user, role);
        }

        public async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
        {
            return await _signInManager.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);
        }
     
        public async Task<IdentityUser?> FindByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<IdentityResult> UpdateAsync(IdentityUser user)
        {
            return await _userManager.UpdateAsync(user);
        }
    }
}
