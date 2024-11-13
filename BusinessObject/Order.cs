using System;
using System.Collections.Generic;

namespace BusinessObject
{
    public class Order
    {
        public int OrderId { get; set; } // Unique identifier for the order
        public int MemberId { get; set; } // ID of the member who placed the order

        public virtual Member Member { get; set; } = null!; // Navigation property to the member

        public virtual ICollection<Cart> CartItems { get; set; } = new List<Cart>(); // Items in the order

        public decimal TotalAmount => CalculateTotalAmount(); // Total price of the order

        public DateTime OrderDate { get; set; } = DateTime.UtcNow; // Date when the order was placed

        // Constructor
        public Order(int orderId, int memberId)
        {
            OrderId = orderId;
            MemberId = memberId;
            CartItems = new List<Cart>();
        }

        // Method to calculate the total amount of the order
        private decimal CalculateTotalAmount()
        {
            decimal total = 0;
            foreach (var cartItem in CartItems)
            {
                total += cartItem.TotalPrice; // Assuming TotalPrice is calculated in Cart
            }
            return total;
        }

        public List<Product> Products
        {
            get
            {
                return CartItems.Select(cart => cart.Product).ToList();
            }
        }
    }
}