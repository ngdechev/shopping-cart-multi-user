using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.server;

namespace ShoppingCartMultiUser.services
{
    internal interface IShoppingCartService
    {
        public string AddCartItem(int id, int productId, int productQuantity);

        public string RemoveCartItem(int clientId, int itemId);
        
        public string UpdateCartItemQuantity(int cartItemId, int newQuantity);
        
        public string ListCartItems(int clientId);
        
        public string Checkout(int clientId);

        public List<CartItem> GetCartItems();
        
        public ClientContainer GetClientContainer(int clientId);
        public void AddNewClientContainer(ClientContainer clientContainer);
    }
}
