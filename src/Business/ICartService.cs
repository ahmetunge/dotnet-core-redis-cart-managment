using System.Threading.Tasks;
using Entities;
using Entities.Dtos;

namespace Business
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(string key);

        Task<Cart> SetCartAsync(string key, CartDto cartDto);

        Task DeleteAsync(string key);
    }
}