using BusinessObject;
using Repositories.ORDER;
using System.Collections.Generic;

namespace Services.ORDER
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepositories _orderRepository;

        public OrderService(IOrderRepositories orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public void AddOrder(Order order)
        {
            _orderRepository.AddOrder(order);
        }

        public void RemoveOrder(int orderId)
        {
            _orderRepository.RemoveOrder(orderId);
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetOrderById(orderId);
        }

        public List<Order> GetAllOrders()
        {
            return _orderRepository.GetAllOrders();
        }

        public List<Order> GetOrdersByMemberId(int memberId)
        {
            return _orderRepository.GetOrdersByMemberId(memberId);
        }
    }
}