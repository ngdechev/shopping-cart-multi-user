using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingCartMultiUser.entity
{
    internal class CartItem
    {
        public int Id { get; set; } = 1;
        public int ProductId { get; set; }
        public int Quantity { get; set; }

        public CartItem(int productId, int quantity)
        {
            Id++;
            ProductId = productId;
            Quantity = quantity;
        }
    }
}
