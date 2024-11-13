using BusinessObject;
using Repositories.CART;
using System.Collections.Generic;

namespace Services.CART
{
    public class CartService : ICartServices
    {
        private readonly ICartRepositories _cartRepository;

        public CartService(ICartRepositories cartRepository)
        {
            _cartRepository = cartRepository; // Dependency injection of the repository
        }

        public void AddCart(Cart cart)
        {
            _cartRepository.AddCart(cart); // Delegate to the repository
        }

        public void RemoveCart(int cartId)
        {
            _cartRepository.RemoveCart(cartId); // Delegate to the repository
        }

        public Cart GetCartByMemberId(int memberId)
        {
            return _cartRepository.GetCartByMemberId(memberId); // Delegate to the repository
        }

        public List<Cart> GetAllCarts()
        {
            return _cartRepository.GetAllCarts(); // Delegate to the repository
        }
    }
}