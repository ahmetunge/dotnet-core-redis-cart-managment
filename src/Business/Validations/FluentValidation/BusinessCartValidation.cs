using Business.Validations.FluentValidation;
using Core.Validation.FluentValidation;
using Entities.Dtos;

namespace Business.Validations.FluentValidation
{
    public class BusinessCartValidation : IBusinessCartValidation
    {
        public bool ValidateCart(CartDto cartDto)
        {
            FluentValidationTool.Validate<CartDto>(new CartValidator(), cartDto);
            foreach (var cartItem in cartDto.Items)
            {
                FluentValidationTool.Validate<CartItemDto>(new CartItemValidator(), cartItem);
            }

            return true;
        }

    }
}