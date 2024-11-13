using BusinessObject;
using System.Collections.Generic;

namespace Repositories.CART
{
    public interface ICartRepositories
    {
        void AddCart(Cart cart);
        void RemoveCart(int cartId);
        Cart GetCartByMemberId(int memberId);
        List<Cart> GetAllCarts();
    }
}