using FluentValidation;
using GaleriaFotosAPI.DTOs;

namespace GaleriaFotosAPI.Validators;

public class SubirFotoRequestValidator : AbstractValidator<SubirFotoRequest>
{
    public SubirFotoRequestValidator()
    {
        RuleFor(x => x.ImagenBase64)
            .NotEmpty().WithMessage("La imagen en Base64 es obligatoria.")
            .WithMessage("El formato de la imagen no es válido. Debe ser JPEG o JPG en base64.");
    }
}