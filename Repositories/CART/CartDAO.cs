using BusinessObject;
using System.Collections.Generic;
using System.Linq;

namespace Repositories.CART
{
    public class CartDAO
    {
        public void AddCart(Cart cart)
        {
            MyStoreContext.Carts.Add(cart); // Assuming you add a static Carts list in MyStoreContext
        }

        public void RemoveCart(int cartId)
        {
            var cart = MyStoreContext.Carts.FirstOrDefault(c => c.CartId == cartId);
            if (cart != null)
            {
                MyStoreContext.Carts.Remove(cart);
            }
        }

        public Cart GetCartByMemberId(int memberId)
        {
            return MyStoreContext.Carts.FirstOrDefault(c => c.MemberId == memberId);
        }

        public List<Cart> GetAllCarts()
        {
            return MyStoreContext.Carts.ToList();
        }
    }
}