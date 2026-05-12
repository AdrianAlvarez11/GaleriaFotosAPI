using AutoMapper;
using GaleriaFotosAPI.DTOs;
using GaleriaFotosAPI.Models;
using GaleriaFotosAPI.Repositories;

namespace GaleriaFotosAPI.Services;

public class FotoService
{
    private readonly Repository<Foto> fotoRepository;
    private readonly UsuarioRepository usuarioRepository;
    private readonly IMapper _mapper;

    public FotoService(Repository<Foto> fotoRepo, UsuarioRepository usuarioRepo, IMapper mapper)
    {
        fotoRepository = fotoRepo;
        usuarioRepository = usuarioRepo;
        _mapper = mapper;
    }

    public async Task<IEnumerable<FotoDto>> GetAllAsync()
    {
        var fotos = await fotoRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<FotoDto>>(fotos);
    }

    public async Task<SubirFotoResponse?> SubirFotoAsync(SubirFotoRequest request,int idusuario, string uploadsPath)
    {
        var usuario = await usuarioRepository.GetByIdAsync(idusuario);
        if (usuario == null) return null;

        var foto = new Foto
        {
            UsuarioId = usuario.Id,
            FechaSubida = DateTime.UtcNow,
        };

        await fotoRepository.AddAsync(foto);
        await fotoRepository.SaveChangesAsync();

        var NombreArchivo = $"{foto.Id}.jpg";

        Directory.CreateDirectory(uploadsPath);
        var rutaArchivo = Path.Combine(uploadsPath, NombreArchivo);
        var base64Data = request.ImagenBase64;
        var imageBytes = Convert.FromBase64String(base64Data);
        await File.WriteAllBytesAsync(rutaArchivo, imageBytes);

        return new SubirFotoResponse()
        {
            Id = foto.Id,
        };
    }

    public async Task<bool> EliminarFotoAsync(int id, int usuarioid, string uploadsPath)
    {
        var foto = await fotoRepository.GetByIdAsync(id);
        if (foto == null) return false;
        if (foto.UsuarioId != usuarioid) return false;

        var NombreArchivo = $"{foto.Id}.jpg";

        var rutaArchivo = Path.Combine(uploadsPath, NombreArchivo);
        if (File.Exists(rutaArchivo)) File.Delete(rutaArchivo);

        await fotoRepository.DeleteAsync(foto);
        await fotoRepository.SaveChangesAsync();
        return true;
    }
}