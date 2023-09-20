using ShoppingCartMultiUser.commands.admin;
using ShoppingCartMultiUser.commands.customer;
using ShoppingCartMultiUser.commands.none;
using ShoppingCartMultiUser.commands.other;
using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.services;

namespace ShoppingCartMultiUser
{
    internal class Application : IApplication
    {
        private EventWaitHandle _closeApp = new(false, EventResetMode.ManualReset);
        // da go pregledam
        private ProductDatabaseService _databaseService;
        private ShoppingCartService _shoppingCartService;
        // da go opravq / premahna
        private List<Product> _products = new();
        private string _filePath = string.Empty;

        private static Dictionary<string, ICommand> _commands;

        public Application()
        {
            _databaseService = new(this);
            _shoppingCartService = new(this);

            _commands = new()
            {
                { "AddProduct", new AddProductCommand(this) },
                { "DeleteProduct", new DeleteProductCommand(this) },
                { "EditProduct", new EditProductCommand(this) },
                { "ListProducts", new ListProductsCommand(this) },

                // Customer Commands
                { "AddCartItem", new AddCartItemCommand(this) },
                { "RemoveCartItem", new RemoveCartItemCommand(this) },
                { "UpdateCartItemQuantity", new UpdateCartItemQuantityCommand(this) },
                { "ListCartItems", new ListCartItemsCommand(this) },
                { "Checkout", new CheckoutCommand(this) },

                // Other
                { "UpdateProductQuantity", new UpdateProductQuantity(this) },

                // None role commands
                { "Exit", new ExitCommand(this) },
                { "Help", new HelpCommand(this) },
                { "Login", new LoginCommand(this) }
            };

            CommandParser.SetCommands(_commands);
        }

        public void Exit()
        {
            _closeApp.Set();

            _databaseService.Cleanup();

            Environment.Exit(0);
        }

        // remove...
        public IDatabaseService GetDatabaseService()
        {
            return _databaseService;
        }

        public IShoppingCartService GetShoppingCartService()
        {
            return _shoppingCartService;
        }

        public string GetFilePath()
        {
            return _filePath;
        }

        public void SetFilePath(string filePath)
        {
            _filePath = filePath;
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public Dictionary<string, ICommand> GetCommands()
        {
            return _commands;
        }
    }
}