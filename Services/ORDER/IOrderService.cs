using BusinessObject;
using System.Collections.Generic;

namespace Services.ORDER
{
    public interface IOrderService
    {
        void AddOrder(Order order);
        void RemoveOrder(int orderId);
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        List<Order> GetOrdersByMemberId(int memberId);
    }
}