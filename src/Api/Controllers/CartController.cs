using System.Threading.Tasks;
using Business;
using Entities;
using Entities.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;

        }


        [HttpGet]
        public async Task<ActionResult<Cart>> GetCart(string key)
        {
            var cart = await _cartService.GetCartAsync(key);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<ActionResult<Cart>> SetCart(CartDto cartDto)
        {
            await _cartService.SetCartAsync(cartDto.Id, cartDto);
            return Ok(cartDto);
        }

        [HttpDelete]
        public async Task DeleteCart(string key)
        {
            await _cartService.DeleteAsync(key);
        }

    }
}