using System;
using System.Collections.Generic;
using System.Text;

namespace TestNinja.Mocking
{
    public class OrderService
    {
        private readonly IStorage _storage;

        public OrderService(IStorage storage)
        {
            _storage = storage;
        }

        public int PlaceOrder(Order order)
        {
            var orderId = _storage.Store(order);

            // other work

            return orderId;
        }
    }

    public class Order
    {

    }

    public interface IStorage
    {
        int Store(Order order);
    }
}
