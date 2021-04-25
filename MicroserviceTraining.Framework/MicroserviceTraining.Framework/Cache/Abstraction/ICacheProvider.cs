using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Cache.Abstraction
{
    public interface ICacheProvider
    {
        Task<T> AddItem<T>(string key, T item);

        Task<T> GetItem<T>(string key);
    }
}
