using GaleriaFotosAPI.DTOs;
using GaleriaFotosAPI.Models;
using GaleriaFotosAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace GaleriaFotosAPI.Services;

public class AuthService
{
    private readonly UsuarioRepository usuarioRepository;
    private readonly IConfiguration config;

    public AuthService(UsuarioRepository usuarioRepo, IConfiguration config)
    {
        usuarioRepository = usuarioRepo;
        this.config = config;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request)
    {
        var existe = await usuarioRepository.GetByNombreUsuarioAsync(request.NombreUsuario);
        if (existe != null) return null;

        var usuario = new Usuario
        {
            NombreUsuario = request.NombreUsuario,
            NombreReal = request.NombreReal,
            PinHash = BCrypt.Net.BCrypt.HashPassword(request.Pin)
        };

        await usuarioRepository.AddAsync(usuario);
        await usuarioRepository.SaveChangesAsync();

        return await GenerarTokensAsync(usuario);
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request)
    {
        var usuario = await usuarioRepository.GetByNombreUsuarioAsync(request.NombreUsuario);
        if (usuario == null) return null;
        if (!BCrypt.Net.BCrypt.Verify(request.Pin, usuario.PinHash)) return null;

        return await GenerarTokensAsync(usuario);
    }

    public async Task<AuthResponse?> RefreshTokenAsync(string refreshToken)
    {
        var usuario = await usuarioRepository.GetByRefreshTokenAsync(refreshToken);
        if (usuario == null) return null;
        if (usuario.RefreshTokenExpiryTime < DateTime.UtcNow) return null;

        return await GenerarTokensAsync(usuario);
    }

    private async Task<AuthResponse> GenerarTokensAsync(Usuario usuario)
    {
        var accessToken = GenerarAccessToken(usuario);
        var newRefreshToken = Guid.NewGuid().ToString();

        usuario.RefreshToken = newRefreshToken;
        usuario.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
        await usuarioRepository.UpdateAsync(usuario);
        await usuarioRepository.SaveChangesAsync();

        return new AuthResponse()
        {
            AccessToken = accessToken,
            RefreshToken = newRefreshToken,
            NombreUsuario = usuario.NombreUsuario,
            NombreReal = usuario.NombreReal
        };
    }

    private string GenerarAccessToken(Usuario usuario)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, usuario.Id.ToString()),
            new Claim(ClaimTypes.Name, usuario.NombreUsuario),
            new Claim("nombreReal", usuario.NombreReal)
        };

        var token = new JwtSecurityToken(
            issuer: config["Jwt:Issuer"],
            audience: config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}