using Entities.Dtos;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class CartValidator : AbstractValidator<CartDto>
    {
        public CartValidator()
        {
            RuleFor(c => c.Id)
            .NotNull();

        }
    }
}