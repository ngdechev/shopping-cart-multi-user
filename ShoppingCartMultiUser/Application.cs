using ShoppingCartMultiUser.commands.admin;
using ShoppingCartMultiUser.entity;
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
        private List<CartItem> _cartItems = new();
        private string _filePath = string.Empty;
        private Dictionary<uint, List<Product>> _shoppingCartMap = new();

        public Application()
        {
            _databaseService = new ProductDatabaseService(this);
            _shoppingCartService = new ShoppingCartService(this);
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

        public List<CartItem> GetCartItems() {
            return _cartItems;
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
            CommandParser.RegisterCommand("AddProduct", new AddProductCommand(this));
            CommandParser.RegisterCommand("DeleteProduct", new AddProductCommand(this));
            CommandParser.RegisterCommand("EditProduct", new AddProductCommand(this));
            CommandParser.RegisterCommand("ListProduct", new ListProductsCommand(this));
            CommandParser.RegisterCommand("Exit", new ExitCommand(this));
            CommandParser.RegisterCommand("Login", new LoginCommand(this));

            Console.Write("> ");
            return CommandParser.ParseCommands(GetRole(), input);
        }
    }
}