using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.server;

namespace ShoppingCartMultiUser.services
{
    internal class ShoppingCartService : IShoppingCartService
    {
        private Application _app;
        private List<CartItem> _cartItems;
        private List<ClientContainer> _clientContainers;

        public ShoppingCartService(Application app)
        {
            _app = app;
            _clientContainers = new List<ClientContainer>();    
        }

        public string AddCartItem(int client_id,int productId, int productQuantity)
        {
            Product? product = GetProductById(productId);

            foreach(var c in _clientContainers) 
            {
                if(c.GetClientId() == client_id)
                {
                    c.GetShoppingCart().Add(new CartItem(productId, productQuantity));

                    return $"[{product.Name}] was added to the shopping cart!";
                }
            }

            return $"There was a problem adding [{product.Name}] to the shopping cart!";
        }

        public string RemoveCartItem(int client_id, int cartItemId)
        {
            CartItem cartItem = GetCartItemById(cartItemId);
            Product product = GetProductById(cartItemId);

            if (cartItem == null)
                return $"Product with id [{cartItemId}] in shopping cart cannot be found!";

            foreach (var c in _clientContainers)
            {
                if (c.GetClientId() == client_id)
                {
                    c.GetShoppingCart().Remove(cartItem);
                }
            }

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

        public string ListCartItems(int client_id)
        {
            foreach (var c in _clientContainers)
                if (c.GetClientId() == client_id)
                    if (c.GetShoppingCart().Count != 0)
                        return c.GetShoppingCart().ToString();

            return "The product list is empty!";
        }

        public string Checkout(int clientId)
        {
            float totalPrice = 0;

            foreach (var c in _clientContainers)
                if (c.GetClientId() == clientId)
                {
                    if (c.GetShoppingCart().Count != 0)
                    {
                        foreach (var cartItem in c.GetShoppingCart())
                        {
                            Product product = GetProductById(cartItem.ProductId);

                            if (product != null)
                            {
                                totalPrice += product.Price * cartItem.Quantity;

                                c.GetShoppingCart().Clear();

                                return $"Your final price is: {totalPrice}";
                            }
                        }
                    }
                }

            return "Sorry, there is no items in your shopping cart!";
        }

        public void AddNewClientContainer( ClientContainer clientContainer)
        {
            _clientContainers.Add(clientContainer);
        }

        public List<CartItem> GetCartItems()
        {
            return _cartItems;
        }

        private Product GetProductById(int productId)
        {
            Product? product = _app.GetProducts().Find(p => p.Id == productId);

            return product;
        }

        private CartItem GetCartItemById(int cartItemId)
        {
            CartItem? cartItem = _cartItems.Find(c => c.Id == cartItemId);

            return cartItem;
        }

        public ClientContainer GetClientContainer(int clientId)
        {
            foreach(ClientContainer clientContainer in _clientContainers)
            {
                if(clientId == clientContainer.GetClientId())
                {
                    return clientContainer;
                }
            }
            return null;
        }
    }
}
