using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.server
{
    internal class ClientContainer
    {
        private List<CartItem>? _shoppingCartProducts;
        private UserRole _currentRole;

        public ClientContainer(Application application)
        {
            _shoppingCartProducts = new List<CartItem>();
            _currentRole = application.GetRole();
        }

        public List<CartItem> GetShoppingCartProducts()
        {
            return _shoppingCartProducts;
        }

        public UserRole GetUserRole() { 
            return _currentRole;
        }
    }
}