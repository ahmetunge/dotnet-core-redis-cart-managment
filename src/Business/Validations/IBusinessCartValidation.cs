using Entities.Dtos;

namespace Business.Validations
{
    public interface IBusinessCartValidation
    {
        bool ValidateCart(CartDto cartDto);
    }
}