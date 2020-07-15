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
using Entities.Dtos;
using Business.Validations;

namespace Business.Tests
{
    public class CartServiceTests
    {
        Mock<ICacheManager> _mockCache;
        Mock<ICartService> _mockCartService;
        Mock<IMapper> _mockMapper;
        Mock<IOptions<RedisConfiguration>> _mockOptions;

        Mock<IBusinessCartValidation> _mockCartValidation;
        public CartServiceTests()
        {
            _mockCache = new Mock<ICacheManager>();
            _mockCartService = new Mock<ICartService>();
            _mockMapper = new Mock<IMapper>();
            _mockOptions = new Mock<IOptions<RedisConfiguration>>();
            _mockOptions.SetupGet(c => c.Value).Returns(new RedisConfiguration());
            _mockCartValidation = new Mock<IBusinessCartValidation>();

        }

        [Fact]
        public async Task GetCart_IfCartNull_ShouldReturnNewInstance()
        {
            string key = "key";

            Cart fakeCart = new Cart();
            fakeCart = null;

            _mockCache.Setup(c => c.GetAsync<Cart>(It.IsAny<string>()))
            .ReturnsAsync(fakeCart);

            CartService cartService = new CartService(_mockCache.Object, _mockMapper.Object, _mockOptions.Object, _mockCartValidation.Object);

            var cart = await cartService.GetCartAsync(key);

            Assert.NotNull(cart);
            Assert.True(cart.Id == key);

        }

        [Fact]
        public async Task GetCart_IfFound_ShouldRetunCart()
        {
            var fakeCart = FakeData.FakeCart;

            _mockCache.Setup(c => c.GetAsync<Cart>(It.IsAny<string>()))
            .ReturnsAsync(fakeCart);

            CartService cartService = new CartService(_mockCache.Object, _mockMapper.Object, _mockOptions.Object, _mockCartValidation.Object);

            var cart = await cartService.GetCartAsync(fakeCart.Id);
            Assert.NotNull(cart);
            Assert.True(cart.Items.Count == 1);

        }


        [Fact]
        public void GetCart_IfKeyNull_ShouldThrowException()
        {
            var fakeCart = FakeData.FakeCart;

            _mockCache.Setup(c => c.GetAsync<Cart>(""))
            .ReturnsAsync(fakeCart);

            CartService cartService = new CartService(_mockCache.Object, _mockMapper.Object, _mockOptions.Object, _mockCartValidation.Object);

            Assert.ThrowsAsync<Exception>(async () => await cartService.GetCartAsync(""));
        }

        [Fact]
        public async Task SetCart_IfSatisfy_ShouldReturnCart()
        {
            var fakeCart = FakeData.FakeCart;
            var fakeCartDto = FakeData.FakeCartDto;

            _mockCartValidation.Setup(v => v.ValidateCart(It.IsAny<CartDto>()))
            .Returns(true);

            _mockMapper.Setup(mp => mp.Map<Cart>(It.IsAny<CartDto>())).Returns(fakeCart);

            _mockCache.Setup(c => c.SetAsync<Cart>(It.IsAny<string>(), It.IsAny<Cart>(), It.IsAny<int>()))
            .ReturnsAsync(true);

            CartService cartService = new CartService(_mockCache.Object, _mockMapper.Object, _mockOptions.Object, _mockCartValidation.Object);


            Cart cart = await cartService.SetCartAsync(fakeCart.Id, fakeCartDto);

            Assert.NotNull(cart);
            Assert.Equal(cart.Id, fakeCartDto.Id);

        }


        [Fact]
        public async Task SetCart_IfNotSuccess_ShouldReturnNull()
        {
            var fakeCart = FakeData.FakeCart;
            var fakeCartDto = FakeData.FakeCartDto;

            _mockCartValidation.Setup(v => v.ValidateCart(It.IsAny<CartDto>()))
            .Returns(true);

            _mockMapper.Setup(mp => mp.Map<Cart>(It.IsAny<CartDto>())).Returns(fakeCart);

            _mockCache.Setup(c => c.SetAsync<Cart>(It.IsAny<string>(), It.IsAny<Cart>(), It.IsAny<int>()))
            .ReturnsAsync(false);

            CartService cartService = new CartService(_mockCache.Object, _mockMapper.Object, _mockOptions.Object, _mockCartValidation.Object);


            Cart cart = await cartService.SetCartAsync(fakeCart.Id, fakeCartDto);

            Assert.Null(cart);
        }


    }
}

public static class FakeData
{
    public static Cart FakeCart
    {
        get
        {
            return new Cart
            {
                Id = "key",
                Items = new List<CartItem>{
                    new CartItem{
                        Id=17,
                        ProductName="Name",
                        Price=1500,
                        Quantity=1
                    }
                }
            };
        }
    }


    public static CartDto FakeCartDto
    {
        get
        {
            return new CartDto
            {
                Id = "key",
                Items = new List<CartItemDto>{
                    new CartItemDto{
                        Id=17,
                        ProductName="Name",
                        Price=1500,
                        Quantity=1
                    }
                }
            };
        }
    }

}