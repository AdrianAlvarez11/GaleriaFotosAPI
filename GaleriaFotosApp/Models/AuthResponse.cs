namespace GaleriaFotosMaui.Models;

public class AuthResponse
{
    public string AccessToken { get; set; } = string.Empty;
    public string RefreshToken { get; set; } = string.Empty;
    public string NombreUsuario { get; set; } = string.Empty;
    public string NombreReal { get; set; } = string.Empty;
}