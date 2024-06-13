using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using RestSharp;

namespace DoctorAppointmentBooking.Presentation.BlazorWebApp.Provider;

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
        try
        {
            var tokenString = await _localStorage.GetItemAsync<string>("authToken");

            if (!string.IsNullOrEmpty(tokenString))
            {
                if (tokenString.Trim().StartsWith("{"))
                {
                    var tokenObject = JsonDocument.Parse(tokenString);
                    if (tokenObject.RootElement.TryGetProperty("Token", out var tokenElement))
                    {
                        tokenString = tokenElement.GetString();
                    }
                }

                var tokenHandler = new JwtSecurityTokenHandler();
                if (tokenHandler.CanReadToken(tokenString))
                {
                    var securityToken = tokenHandler.ReadJwtToken(tokenString);

                    var nameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name");
                    var uniqueName = nameClaim?.Value; 
                    
                    var roleClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "role");
                    var role = roleClaim?.Value; 
                    
                    var idUserClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid");
                    var idUser = idUserClaim?.Value; 

                    var identity = new ClaimsIdentity(new[]
                    {
                        new Claim(ClaimTypes.Name, uniqueName),
                        new Claim(ClaimTypes.Role, role),
                        new Claim(ClaimTypes.NameIdentifier, idUser)
                    }, "apiauth");

                    var user = new ClaimsPrincipal(identity);
                    return await Task.FromResult(new AuthenticationState(user));
                }
            }
        }
        catch (Exception ex)
        {
            NotifyUserLogout();
        }

        NotifyUserLogout();
        return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
    }

    public void NotifyUserAuthentication(string token)
    {
        if (!string.IsNullOrEmpty(token))
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadJwtToken(token);

            var nameClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "unique_name");
            var uniqueName = nameClaim?.Value; 
                    
            var roleClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "role");
            var role = roleClaim?.Value; 
                    
            var idUserClaim = securityToken.Claims.FirstOrDefault(c => c.Type == "nameid");
            var idUser = idUserClaim?.Value; 

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, uniqueName),
                new Claim(ClaimTypes.Role, role),
                new Claim(ClaimTypes.NameIdentifier, idUser)
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