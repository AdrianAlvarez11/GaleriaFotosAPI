using GaleriaFotosAPI.Data;
using GaleriaFotosAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GaleriaFotosAPI.Repositories;

public class UsuarioRepository : Repository<Usuario>
{
    public UsuarioRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Usuario?> GetByNombreUsuarioAsync(string nombreUsuario)
    { 
       return await _dbSet.FirstOrDefaultAsync(u => u.NombreUsuario == nombreUsuario);
    }

    public async Task<Usuario?> GetByRefreshTokenAsync(string refreshToken)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
    }
}