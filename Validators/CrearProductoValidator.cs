using FluentValidation;
using InventarioAPI.DTOs.Productos;

namespace InventarioAPI.Validators
{
    public class CrearProductoValidator : AbstractValidator<CrearProductoDto>
    {
        public CrearProductoValidator()
        {
            RuleFor(x => x.Nombre)
                .NotEmpty()
                .MaximumLength(100);

            RuleFor(x => x.Precio)
                .GreaterThan(0);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.CategoriaId)
                .GreaterThan(0);
        }
    }
}
