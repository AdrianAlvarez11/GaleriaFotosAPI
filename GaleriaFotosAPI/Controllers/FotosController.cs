using FluentValidation;
using GaleriaFotosAPI.DTOs;
using GaleriaFotosAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GaleriaFotosAPI.Controllers;

[ApiController]
[Route("fotos")]
[Authorize]
public class FotosController : ControllerBase
{
    private readonly FotoService fotoService;
    private readonly IWebHostEnvironment env;
    private readonly IValidator<SubirFotoRequest> subirValidator;

    public FotosController(
        FotoService fotoService,
        IWebHostEnvironment env,
        IValidator<SubirFotoRequest> subirValidator)
    {
        this.fotoService = fotoService;
        this.env = env;
        this.subirValidator = subirValidator;
    }

    [HttpGet]
    public async Task<IActionResult> GetFotos()
    {
        var fotos = await fotoService.GetAllAsync();
        return Ok(fotos);
    }

    [HttpPost]
    public async Task<IActionResult> PostFoto(SubirFotoRequest request)
    {
        var validation = await subirValidator.ValidateAsync(request);

        if (!validation.IsValid)
            return BadRequest(validation.Errors.Select(e => e.ErrorMessage));

        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario);

        if (idUsuario == 0)
            return Unauthorized();

        var uploadsPath = Path.Combine(env.WebRootPath, "Uploads");
        var result = await fotoService.SubirFotoAsync(request, idUsuario, uploadsPath);

        if (result == null)
            return BadRequest(new[] { "No se pudo subir la foto. Usuario no encontrado." });

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteFoto(int id)
    {
        int.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier), out int idUsuario);
        if (idUsuario == 0)
            return Unauthorized();

        var uploadsPath = Path.Combine(env.WebRootPath, "Uploads");
        var success = await fotoService.EliminarFotoAsync(id, idUsuario, uploadsPath);

        if (!success)
            return NotFound();

        return Ok();
    }
}