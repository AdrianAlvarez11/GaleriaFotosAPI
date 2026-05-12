namespace GaleriaFotosAPI.Models;

public class Foto
{
    public int Id { get; set; }
    public int UsuarioId { get; set; }
    public Usuario Usuario { get; set; } = null!;
    public DateTime FechaSubida { get; set; } = DateTime.UtcNow;
}