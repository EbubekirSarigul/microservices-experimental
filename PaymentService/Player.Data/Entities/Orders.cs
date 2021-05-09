using MicroserviceTraining.Framework.Data;
using Player.Data.Enums;
using Player.Data.Events;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Player.Data.Entities
{
    public class Orders : Entity
    {
        private Orders() { }

        public Orders(int totalPrice, Guid paymentId, Guid playerId)
        {
            TotalPrice = totalPrice;
            PaymentId = paymentId;
            PlayerId = playerId;
            OrderStatus = OrderStatusEnum.PENDING.ToString();
            _orderDetails = new List<OrderDetails>();
        }

        public int TotalPrice { get; private set; }

        public Guid PaymentId { get; private set; }

        public Guid PlayerId { get; private set; }

        public string OrderStatus { get; set; }

        private readonly List<OrderDetails> _orderDetails;

        public IReadOnlyCollection<OrderDetails> OrderDetails => _orderDetails;

        public void AddOrderDetail(Guid tournamentId)
        {
            var orderDetail = new OrderDetails(tournamentId, Id);
            _orderDetails.Add(orderDetail);
        }

        public void SetStatusCompleted()
        {
            OrderStatus = OrderStatusEnum.COMPLETED.ToString();

            AddDomainEvent(new PaymentCompletedDomainEvent(PlayerId, _orderDetails.Select(x => x.TournamentId).ToList()));
        }
    }
}
