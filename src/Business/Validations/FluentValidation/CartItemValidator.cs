using Business.Helpers;
using Entities.Dtos;
using FluentValidation;

namespace Business.Validations.FluentValidation
{
    public class CartItemValidator : AbstractValidator<CartItemDto>
    {
        public CartItemValidator()
        {
            RuleFor(ci => ci.Id)
            .NotNull()
            .NotEqual(0);

            RuleFor(ci => ci.ProductName)
            .NotNull();

            RuleFor(ci => ci.Price)
            .GreaterThan(0)
            .WithMessage(Messages.PriceLessThanZeroError);

            RuleFor(ci => ci.Quantity)
            .InclusiveBetween(1, int.MaxValue)
            .WithMessage(Messages.QuantityLessThanOneError);
        }
    }
}