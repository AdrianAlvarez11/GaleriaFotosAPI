namespace GaleriaFotosAPI.DTOs;

public class RegisterRequest
{
    public string NombreUsuario { get; set; } = null!;
    public string NombreReal { get; set; } = null!;
    public string Pin { get; set; } = null!;
}

public class LoginRequest
{
    public string NombreUsuario { get; set; } = null!;
    public string Pin { get; set; } = null!;
}

public class RefreshTokenRequest
{
    public string RefreshToken { get; set; } = null!;
}

public class AuthResponse
{
    public string AccessToken { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string NombreUsuario { get; set; } = null!;
    public string NombreReal { get; set; } = null!;
}

public class UsuarioDto
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string NombreReal { get; set; } = null!;
}