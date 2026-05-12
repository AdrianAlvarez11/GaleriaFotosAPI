using FluentValidation;
using GaleriaFotosAPI.DTOs;

namespace GaleriaFotosAPI.Validators;

public class SubirFotoRequestValidator : AbstractValidator<SubirFotoRequest>
{
    public SubirFotoRequestValidator()
    {
        RuleFor(x => x.ImagenBase64)
            .NotEmpty().WithMessage("La imagen en Base64 es obligatoria.")
            .Must(x => x.EndsWith(".jpeg") || x.EndsWith(".jpg"))
            .WithMessage("El formato de la imagen no es válido. Debe ser JPEG o JPG en base64.");
    }
}