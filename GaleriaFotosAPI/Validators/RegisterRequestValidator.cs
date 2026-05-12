using FluentValidation;
using GaleriaFotosAPI.DTOs;

namespace GaleriaFotosAPI.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.NombreUsuario)
            .NotEmpty().WithMessage("El nombre de usuario es obligatorio.")
            .MaximumLength(50).WithMessage("El nombre de usuario no puede tener más de 50 caracteres.");

        RuleFor(x => x.NombreReal)
            .NotEmpty().WithMessage("El nombre real es obligatorio.")
            .MaximumLength(100).WithMessage("El nombre real no puede tener más de 100 caracteres.");

        RuleFor(x => x.Pin)
            .NotEmpty().WithMessage("El PIN es obligatorio.")
            .Matches(@"^\d{4,6}$").WithMessage("El PIN debe tener entre 4 y 6 dígitos numéricos.");
    }
}