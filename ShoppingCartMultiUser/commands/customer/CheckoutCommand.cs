using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.customer
{
    [AllowedRoles(UserRole.Customer)]
    internal class CheckoutCommand : ICommand
    {
        Application _application;

        public CheckoutCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            return _application.GetShoppingCartService().Checkout();
        }

        public string GetHelp()
        {
            return $"{GetName}()";
        }

        public string GetName()
        {
            return "Checkout";
        }
    }
}
