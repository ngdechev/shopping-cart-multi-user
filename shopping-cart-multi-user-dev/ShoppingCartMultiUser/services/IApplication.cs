using ShoppingCartMultiUser.entity;

namespace ShoppingCartMultiUser.services
{
    internal interface IApplication
    {
        public void Exit();

        public IDatabaseService GetDatabaseService();

        public IShoppingCartService GetShoppingCartService();

        public string GetFilePath();

        public void SetFilePath(string filePath);

        public List<Product> GetProducts();
        
        public Dictionary<string, ICommand> GetCommands();
    }
}
