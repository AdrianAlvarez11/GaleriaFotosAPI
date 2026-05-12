using FluentValidation;
using GaleriaFotosAPI.DTOs;

namespace GaleriaFotosAPI.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.NombreUsuario)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio.");

        RuleFor(x => x.Pin)
            .NotEmpty().WithMessage("El PIN es obligatorio.")
            .Matches(@"^\d{4,6}$").WithMessage("El PIN debe tener entre 4 y 6 dígitos numéricos.");
    }
}