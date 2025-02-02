using FluentValidation;
using SOI.Application.Commands;

namespace SOI.Application.Validations;

public class CrearOrdenValidator : AbstractValidator<CrearOrdenCommand>
{
    public CrearOrdenValidator()
    {
        RuleFor(x => x.CuentaId)
            .GreaterThan(0)
            .WithMessage("La CuentaId debe ser mayor a 0.");

        RuleFor(x => x.ActivoId)
            .GreaterThan(0)
            .WithMessage("El ActivoId debe ser mayor a 0.");

        RuleFor(x => x.Cantidad)
            .GreaterThan(0)
            .WithMessage("La cantidad debe ser mayor a 0.");

        RuleFor(x => x.Precio)
            .GreaterThan(0)
            .When(x => x.Precio.HasValue)
            .WithMessage("El precio debe ser mayor a 0.");
        
        RuleFor(x => x.Operacion)
            .NotEmpty()
            .WithMessage("La operación es obligatoria.")
            .Must(op => op == 'V' || op == 'C')
            .WithMessage("La operación debe ser 'V' (Venta) o 'C' (Compra).");
            
    }
}