using ShoppingCartMultiUser.commands.admin;
using ShoppingCartMultiUser.commands.customer;
using ShoppingCartMultiUser.commands.none;
using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser
{
    internal class Application : IApplication
    {
        //private EventWaitHandle _closeApp = new(false, EventResetMode.ManualReset);
        private IDatabaseService _databaseService;
        private IShoppingCartService _shoppingCartService;
        //private List<CommandItem> _commands = new();
        private UserRole _role = UserRole.None;
        private List<UserRole> _userRoles = new();
        private List<Product> _products = new();
        private List<CartItem> _shoppingCart;
        private string _filePath = string.Empty;
        private Dictionary<uint, List<Product>> _shoppingCartMap = new();
        private ClientContainer _clientContainer;

        public Application()
        {
            _databaseService = new ProductDatabaseService(this);
            _shoppingCartService = new ShoppingCartService(this);
            _clientContainer = new(this);

            _shoppingCart = _clientContainer.GetShoppingCartProducts(); 
        }

        public void Exit()
        {
            //_closeApp.Set();

            _databaseService.Cleanup();

            Environment.Exit(0);
        }

        public IDatabaseService GetDatabaseService()
        {
            return _databaseService;
        }

        public IShoppingCartService GetShoppingCartService()
        {
            return _shoppingCartService;
        }

        /*
         public List<CommandItem> GetCommands()
        {
            return _commands;
        }
         */
        public UserRole GetRole()
        {
            return _role;
        }

        public string SetRole(UserRole role)
        {
            _role = role;

            return $"Role is set to {_role}";
        }

        public List<UserRole> GetUserRoles()
        {
            return _userRoles;
        }

        UserRole IApplication.GetRole()
        {
            throw new NotImplementedException();
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

        public List<CartItem> GetShoppingCart() {
            return _shoppingCart;
        }

        public Dictionary<uint, List<Product>> GetShoppingCartMap(uint id)
        {            
            return _shoppingCartMap;
        }

        public void PrintMessage(string msg)
        {
            Console.WriteLine(msg);
        }
        public string Run(string input)
        {
            // Admin commands
            CommandParser.RegisterCommand("AddProduct", new AddProductCommand(this));
            CommandParser.RegisterCommand("DeleteProduct", new AddProductCommand(this));
            CommandParser.RegisterCommand("EditProduct", new AddProductCommand(this));
            CommandParser.RegisterCommand("ListProduct", new ListProductsCommand(this));
            
            // Customer Commands
            CommandParser.RegisterCommand("AddCartItem", new AddCartItemCommand(this));
            CommandParser.RegisterCommand("RemoveCartItem", new RemoveCartItemCommand(this));
            CommandParser.RegisterCommand("UpdateCartItemQuantity", new UpdateCartItemQuantityCommand(this));
            CommandParser.RegisterCommand("ListCartItems", new ListCartItemsCommand(this));

            // None role commands
            CommandParser.RegisterCommand("Exit", new ExitCommand(this));
            CommandParser.RegisterCommand("Help", new HelpCommand(this));
            CommandParser.RegisterCommand("Login", new LoginCommand(this));

            // Other commands - WarehouseWorker role
            //CommandParser.RegisterCommand("UpdateQuantity", new UpdateQuantityCommand(this));

            Console.Write("> ");
            return CommandParser.ParseCommands(GetRole(), input);
        }
    }
}