using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.customer
{
    [AllowedRoles(UserRole.Customer)]
    internal class ListCartItemsCommand : ICommand
    {
        Application _application;

        public ListCartItemsCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args)
        {
            return _application.GetCartItems().Count() == 0 ? "There is no products in your shopping cart!" : _application.GetShoppingCartService().ListCartItems();
        }

        public string GetHelp()
        {
            return $"{GetName}()";
        }

        public string GetName()
        {
            return "ListCartItems";
        }
    }
}
