namespace GaleriaFotosAPI.DTOs;

public class SubirFotoRequest
{
    public string ImagenBase64 { get; set; } = null!;
}

public class FotoDto
{
    public int Id { get; set; }
    public string NombreUsuario { get; set; } = null!;
    public string NombreArchivo { get; set; } = null!;
    public DateTime FechaSubida { get; set; }
}

public class SubirFotoResponse
{
    public int Id { get; set; }
}