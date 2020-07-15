using System.Threading.Tasks;
using Core.Cache;
using Moq;
using Business;
using Entities;
using AutoMapper;
using Xunit;
using Core.Cache.Redis;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System;
using Business.Validations.FluentValidation;
using FluentValidation.TestHelper;
using Entities.Dtos;

namespace Business.Tests
{
    public class CartValidationTest
    {
        private CartValidator _cartValidator;
        public CartValidationTest()
        {
            _cartValidator = new CartValidator();
        }
        [Fact]
        public void CartValidator_IfIdNull_ShouldHaveError()
        {
            CartDto cart = new CartDto();

            var result = _cartValidator.TestValidate(cart);
            result.ShouldHaveValidationErrorFor(c => c.Id);
        }

        [Fact]
        public void CartValidator_IfIdNotNull_ShouldNotHaveError()
        {
            CartDto cart = new CartDto()
            {
                Id = "id"
            };

            var result = _cartValidator.TestValidate(cart);
            result.ShouldNotHaveValidationErrorFor(c => c.Id);
        }
    }
}