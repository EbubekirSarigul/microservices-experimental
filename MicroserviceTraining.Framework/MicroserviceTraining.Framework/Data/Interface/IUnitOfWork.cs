using System.Threading;
using System.Threading.Tasks;

namespace MicroserviceTraining.Framework.Data.Interface
{
    public interface IUnitOfWork
    {
        Task<bool> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
