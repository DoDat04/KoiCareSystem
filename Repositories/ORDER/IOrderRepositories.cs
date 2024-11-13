using BusinessObject;
using System.Collections.Generic;

namespace Repositories.ORDER
{
    public interface IOrderRepositories
    {
        void AddOrder(Order order);
        void RemoveOrder(int orderId);
        Order GetOrderById(int orderId);
        List<Order> GetAllOrders();
        List<Order> GetOrdersByMemberId(int memberId);
    }
}