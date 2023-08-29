﻿using ShoppingCartMultiUser.entity;

namespace ShoppingCartMultiUser.services
{
    internal class ShoppingCartService : IShoppingCartService
    {
        private Application _app;
        private List<CartItem> _cartItems;

        public ShoppingCartService(Application app)
        {
            _app = app;
            _cartItems = _app.GetCartItems();
        }

        public string AddCartItem(int productId, int productQuantity)
        {
            Product? product = GetProductById(productId);

            _cartItems.Add(new CartItem(productId, productQuantity));
            
            return $"[{product.Name}] was added to the shopping cart!";
        }

        public string RemoveCartItem(int cartItemId)
        {
            CartItem? cartItem = GetCartItemById(cartItemId);
            Product product = GetProductById(cartItemId);

            if (cartItem == null)
                return $"Product with id [{cartItemId}] in shopping cart cannot be found!";

            _cartItems.Remove(cartItem);

            return $"[{product.Name}] was removed from shopping cart!";
        }

        public string UpdateCartItemQuantity(int cartItemId, int newQuantity)
        {
            CartItem cartItem = GetCartItemById(cartItemId);
            Product product = GetProductById(cartItemId);

            if (cartItem == null)
                return $"Product with id [{cartItemId}] in shopping cart cannot be found!";
                cartItem.Quantity = newQuantity;

            return $"Quantity of [{product.Name}] was updated to {newQuantity}!";
        }

        public string ListCartItems()
        {
            foreach (CartItem cartItem in _app.GetCartItems())
                if (_app.GetCartItems().Count != 0)
                    return cartItem.ToString();

            return "The product list is empty!";
        }

        public string Checkout()
        {
            throw new NotImplementedException(); 
        }

        private Product GetProductById(int productId)
        {
            Product? product = _app.GetProducts().Find(p => p.Id == productId);

            return product;
        }

        private CartItem GetCartItemById(int cartItemId)
        {
            CartItem? cartItem = _app.GetCartItems().Find(c => c.Id == cartItemId);

            return cartItem;
        }
    }
}
