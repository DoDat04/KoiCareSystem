using BusinessObject;
using System.Collections.Generic;

namespace Services.CART
{
    public interface ICartServices
    {
        void AddCart(Cart cart);
        void RemoveCart(int cartId);
        Cart GetCartByMemberId(int memberId);
        List<Cart> GetAllCarts();
    }
}