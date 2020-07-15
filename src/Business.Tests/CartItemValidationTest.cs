using Business.Helpers;
using Business.Validations.FluentValidation;
using Entities.Dtos;
using FluentValidation.TestHelper;
using Xunit;

namespace Business.Tests
{
    public class CartItemValidationTest
    {
        CartItemValidator _cartItemValidator;
        public CartItemValidationTest()
        {
            _cartItemValidator = new CartItemValidator();
        }

        [Fact]
        public void CartItemValidator_IfIdNotNull_ShouldNotHaveError()
        {
            CartItemDto cartItem = new CartItemDto()
            {
                Id = 1,
            };

            var result = _cartItemValidator.TestValidate(cartItem);
            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CartItemValidator_IfIdNull_ShouldHaveError()
        {
            CartItemDto cartItem = new CartItemDto();

            var result = _cartItemValidator.TestValidate(cartItem);
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CartItemValidator_IfSatisfy_ShouldNotHaveError()
        {
            CartItemDto cartItem = new CartItemDto();
            cartItem.Price = 10;
            cartItem.ProductName = "ProductName";
            cartItem.Quantity = 2;

            var result = _cartItemValidator.TestValidate(cartItem);

            result.ShouldNotHaveValidationErrorFor(c => c.ProductName);
            result.ShouldNotHaveValidationErrorFor(c => c.Price);
            result.ShouldNotHaveValidationErrorFor(c => c.Quantity);
        }

        [Fact]
        public void CartItemValidator_IfAllPropertiesNull_ShouldHaveError()
        {
            CartItemDto cartItem = new CartItemDto();

            var result = _cartItemValidator.TestValidate(cartItem);

            result.ShouldHaveValidationErrorFor(c => c.ProductName);
            result.ShouldHaveValidationErrorFor(c => c.Price);
            result.ShouldHaveValidationErrorFor(c => c.Quantity);
        }


        [Fact]
        public void CartItemValidator_IfPriceLessThanOne_ShouldHaveError()
        {
            CartItemDto cartItem = new CartItemDto();
            cartItem.Price = 0;

            var result = _cartItemValidator.TestValidate(cartItem);

            result.ShouldHaveValidationErrorFor(c => c.Price).WithErrorMessage(Messages.PriceLessThanZeroError);
        }


        [Fact]
        public void CartItemValidator_IfQuantityOutOfRange_ShouldHaveError()
        {
            CartItemDto cartItem = new CartItemDto();
            cartItem.Quantity = 0;

            var result = _cartItemValidator.TestValidate(cartItem);

            result.ShouldHaveValidationErrorFor(c => c.Quantity).WithErrorMessage(Messages.QuantityLessThanOneError);
        }
    }
}