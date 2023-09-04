using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.server
{
    internal class ClientContainer
    {
        private List<CartItem>? _shoppingCart;
        private UserRole _currentRole = UserRole.None;
        private int _clientId = 0;

        public ClientContainer(int clientId)
        {
            _shoppingCart = new();
            
            _clientId = clientId;
        }

        public List<CartItem> GetShoppingCart()
        {
            return _shoppingCart;
        }

        public string SetUserRole(UserRole role)
        {
            _currentRole = role;

            return $"Role is set to {_currentRole}!";
        }

        public UserRole GetUserRole() { 
            return _currentRole;
        }

        public int GetClientId()
        {
            return _clientId;
        }
    }
}