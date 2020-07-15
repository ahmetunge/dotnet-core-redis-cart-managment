using System;
using System.Threading.Tasks;
using AutoMapper;
using Business.Helpers;
using Business.Validations;
using Business.Validations.FluentValidation;
using Core.Cache;
using Core.Cache.Redis;
using Core.Validation.FluentValidation;
using Entities;
using Entities.Dtos;
using Microsoft.Extensions.Options;

namespace Business
{
    public class CartService : ICartService
    {
        private readonly ICacheManager _cache;
        private readonly IMapper _mapper;
        private readonly IBusinessCartValidation _cartValidation;
        private readonly int _expiry;
        public CartService(
            ICacheManager cache,
            IMapper mapper,
            IOptions<RedisConfiguration> options,
            IBusinessCartValidation cartValidation
            )
        {
            _mapper = mapper;
            _cartValidation = cartValidation;
            _cache = cache;

            var config = options.Value;
            _expiry = config == null ? 30 : config.Expiry;

        }

        public async Task DeleteAsync(string key)
        {
            await _cache.DeleteAsync(key.ToLower());
        }

        public async Task<Cart> GetCartAsync(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception(Messages.NullKeyError);

            Cart cart = await _cache.GetAsync<Cart>(key.ToLower());

            return cart ?? new Cart(key);
        }

        public async Task<Cart> SetCartAsync(string key, CartDto cartDto)
        {

            _cartValidation.ValidateCart(cartDto);

            var cart = _mapper.Map<Cart>(cartDto);

            bool isAdded = await _cache.SetAsync<Cart>(key.ToLower(), cart, _expiry);

            return isAdded == true ? cart : null;
        }

    }
}