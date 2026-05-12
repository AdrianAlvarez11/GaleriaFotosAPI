using GaleriaFotosMaui.Models;
using System.Net.Http.Json;

namespace GaleriaFotosMaui.Services;

public class AuthService
{
    public AuthService(HttpClient client)
    {
        this.client = client;
    }

    private readonly HttpClient client;

    public async Task GuardarTokensAsync(string accessToken, string refreshToken, string nombreUsuario, string nombreReal)
    {
        await SecureStorage.SetAsync("access_token", accessToken);
        await SecureStorage.SetAsync("refresh_token", refreshToken);
        Preferences.Set("nombre_usuario", nombreUsuario);
        Preferences.Set("nombre_real", nombreReal);

        client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", accessToken);

    }

    public async Task<string?> GetAccessTokenAsync()
    {
        return await SecureStorage.GetAsync("access_token");
    }

    public async Task<string?> GetRefreshTokenAsync()
    {
        return await SecureStorage.GetAsync("refresh_token");
    }

    public string? GetNombreUsuario()
    {
        return Preferences.Get("nombre_usuario", null);
    }

    public string? GetNombreReal()
    {
        return Preferences.Get("nombre_real", null);
    }

    public async Task<AuthResponse?> LoginAsync(string nombreUsuario, string pin)
    {
        try
        {
            var body = new { NombreUsuario = nombreUsuario, Pin = pin };
            var response = await client.PostAsJsonAsync("auth/login", body);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    public async Task<AuthResponse?> RegisterAsync(string nombreUsuario, string nombreReal, string pin)
    {
        try
        {
            var body = new { NombreUsuario = nombreUsuario, NombreReal = nombreReal, Pin = pin };
            var response = await client.PostAsJsonAsync("auth/register", body);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
    {
        try
        {
            var body = new { RefreshToken = refreshToken };
            var response = await client.PostAsJsonAsync("auth/refresh", body);
            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadFromJsonAsync<AuthResponse>();
        }
        catch (Exception)
        {
            return null;
        }
    }

    public async Task CerrarSesionAsync()
    {
        SecureStorage.Remove("access_token");
        SecureStorage.Remove("refresh_token");
        Preferences.Remove("nombre_usuario");
        Preferences.Remove("nombre_real");

        client.DefaultRequestHeaders.Authorization = null;
    }

    public async Task<bool> EstaAutenticadoAsync()
    {
        var token = await SecureStorage.GetAsync("access_token");
        return !string.IsNullOrEmpty(token);
    }
}