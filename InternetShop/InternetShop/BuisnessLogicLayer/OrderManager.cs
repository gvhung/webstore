using System.Collections.Generic;
using Entities;
using Repository;

namespace BuisnessLogicLayer
{
    public class OrderManager : IBuisnessLogicLayer<Order, int>
    {
        private OrderRepository orderRepository;

        public OrderManager()
        {
            orderRepository = new OrderRepository();
        }

        public Order Read(int Id)
        {
            return orderRepository.Read(Id);
        }

        public IEnumerable<Order> ReadAll()
        {
            return orderRepository.ReadAll();
        }

        public void Update(Order order)
        {
            orderRepository.Update(order);
        }

        public void Delete(int Id)
        {
            orderRepository.Delete(Id);
        }

        public void Create(Order entity)
        {
            orderRepository.Create(entity);
        }
    }
}
