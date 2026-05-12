namespace GaleriaFotosAPI.Models;

public class Usuario
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = string.Empty;
    public string NombreReal { get; set; } = string.Empty;
    public string PinHash { get; set; } = string.Empty;
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
    public ICollection<Foto> Fotos { get; set; } = new List<Foto>();
}