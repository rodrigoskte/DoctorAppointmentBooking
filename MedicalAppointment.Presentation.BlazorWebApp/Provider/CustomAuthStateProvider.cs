using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;

namespace MedicalAppointment.Presentation.BlazorWebApp.Provider;

public class CustomAuthStateProvider : AuthenticationStateProvider
{
    private readonly ILocalStorageService _localStorage;
    private readonly IRestClient _restClient;

    public CustomAuthStateProvider(ILocalStorageService localStorage, IRestClient restClient)
    {
        _localStorage = localStorage;
        _restClient = restClient;
    }

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
    {
        NotifyUserLogout();
        var token = await _localStorage.GetItemAsync<string>("authToken");
        var identity = string.IsNullOrEmpty(token) ? new ClaimsIdentity() : new ClaimsIdentity(new[]
        {
            new Claim(ClaimTypes.Name, "User")
        }, "apiauth");

        var user = new ClaimsPrincipal(identity);
        return await Task.FromResult(new AuthenticationState(user));
    }

    public void NotifyUserAuthentication(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);
        
            var roleClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "role");
            var role = roleClaim?.Value; // Obter o valor do claim de role
        
            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, "User"),
                new Claim(ClaimTypes.Role, role) // Adicionar o claim de role à identidade
            }, "apiauth");
            
            var user = new ClaimsPrincipal(identity);
            NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
        }
    }

    public void NotifyUserLogout()
    {
        var identity = new ClaimsIdentity();
        var user = new ClaimsPrincipal(identity);
        NotifyAuthenticationStateChanged(Task.FromResult(new AuthenticationState(user)));
    }
}