using MicroserviceTraining.Framework.Data.Interface;
using Microsoft.EntityFrameworkCore;
using Player.Data.Context;
using Player.Data.Entities;
using System;
using System.Threading.Tasks;

namespace Player.Data.Repositories
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly PaymentContext _paymentContext;

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _paymentContext;
            }
        }

        public PaymentRepository(PaymentContext paymentContext)
        {
            _paymentContext = paymentContext;
        }

        public async Task<Orders> AddOrder(Orders order)
        {
            var result = await _paymentContext.AddAsync(order);
            return result.Entity;
        }

        public async Task<Orders> GetOrder(string paymentId)
        {
            var order = await _paymentContext.Orders.SingleOrDefaultAsync(x => x.PaymentId == Guid.Parse(paymentId));
            if (order != null)
            {
                await _paymentContext.Entry(order).Collection(x => x.OrderDetails).LoadAsync();
            }

            return order;
        }
    }
}
