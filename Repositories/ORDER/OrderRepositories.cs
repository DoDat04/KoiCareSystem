using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.ORDER
{
    public class OrderRepositories : IOrderRepositories
    {
        public void AddOrder(Order order)
        {
            MyStoreContext.Orders.Add(order); // Assuming you add a static Orders list in MyStoreContext
        }

        public void RemoveOrder(int orderId)
        {
            var order = MyStoreContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
            if (order != null)
            {
                MyStoreContext.Orders.Remove(order);
            }
        }

        public Order GetOrderById(int orderId)
        {
            return MyStoreContext.Orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public List<Order> GetAllOrders()
        {
            return MyStoreContext.Orders.ToList();
        }

        public List<Order> GetOrdersByMemberId(int memberId)
        {
            return MyStoreContext.Orders.Where(o => o.MemberId == memberId).ToList();
        }
    }
}