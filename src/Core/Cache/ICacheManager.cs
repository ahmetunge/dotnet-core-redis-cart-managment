using System.Threading.Tasks;

namespace Core.Cache
{
    public interface ICacheManager
    {
        Task<T> GetAsync<T>(string key) where T : class, new();
        Task<object> GetAsync(string key);

        Task<bool> SetAsync<T>(string key, T data, int duration);

        Task<bool> SetAsync(string key, object data, int duration);

        Task<bool> DeleteAsync(string key);
    }
}