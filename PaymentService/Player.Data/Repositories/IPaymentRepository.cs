using MicroserviceTraining.Framework.Data.Interface;
using Player.Data.Entities;
using System.Threading.Tasks;

namespace Player.Data.Repositories
{
    public interface IPaymentRepository : IRepository
    {
        Task<Orders> AddOrder(Orders order);

        Task<Orders> GetOrder(string paymentId);
    }
}
