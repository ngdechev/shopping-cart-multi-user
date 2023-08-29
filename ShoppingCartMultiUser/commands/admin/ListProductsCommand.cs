using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.admin
{
    [AllowedRoles(UserRole.Admin)]
    internal class ListProductsCommand : ICommand
    {
        private Application _application;

        public ListProductsCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args)
        {
            return _application.GetProducts().Count == 0 ? "Product list is empty!" : _application.GetDatabaseService().ListProducts();
        }

        public string GetHelp()
        {
            return $"{GetName()}()";
        }

        public string GetName()
        {
            return "ListProducts";
        }
    }
}
