using ShoppingCartMultiUser.entity;

namespace ShoppingCartMultiUser.server
{
    internal class ShoppingCart
    {
        private int _id = 0;
        private List<CartItem>? _shoppingCartProducts;

        public ShoppingCart(int id)
        {
            id++;
            _shoppingCartProducts = new();
        }

        public List<CartItem> GetShoppingCart()
        {
            return _shoppingCartProducts;
        }
    }
}
