using System;

namespace BusinessObject
{
    public class Cart
    {
        public int CartId { get; set; }

        // Associate the cart with a member
        public int MemberId { get; set; }

        // The product currently in the cart
        public Product Product { get; set; } = null!;

        // The quantity of the product in the cart
        public short Quantity { get; set; }

        // This could be calculated from the product's price
        public decimal TotalPrice => (Product.UnitPrice ?? 0) * Quantity;

        // Optional: You might want to add a constructor if needed
        public Cart(int cartId, int memberId, Product product, short quantity)
        {
            CartId = cartId;
            MemberId = memberId;
            Product = product;
            Quantity = quantity;
        }       
    }
}