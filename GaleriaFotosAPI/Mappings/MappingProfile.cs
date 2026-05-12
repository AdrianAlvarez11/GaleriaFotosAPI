using AutoMapper;
using GaleriaFotosAPI.DTOs;
using GaleriaFotosAPI.Models;

namespace GaleriaFotosAPI.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Usuario, UsuarioDto>();

        CreateMap<Foto, FotoDto>()
            .ForMember(dest => dest.NombreUsuario, opt => opt.MapFrom(src => src.Usuario.NombreUsuario));
    }
}