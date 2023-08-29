namespace ShoppingCartMultiUser.services
{
    internal interface IShoppingCartService
    {
        public string AddCartItem(int productId, int productQuantity);
        public string RemoveCartItem(int itemId);
        public string UpdateCartItemQuantity(int cartItemId, int newQuantity);
        public string ListCartItems();
        public string Checkout();

    }
}
