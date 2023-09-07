using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.customer
{
    [AllowedRoles(UserRole.Customer)]
    internal class RemoveCartItemCommand : ICommand
    {
        Application _application;

        public RemoveCartItemCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            if (args.Length < 1) 
                return ($"Invalid number of arguments. Usage: {GetHelp()}");

            int productId = int.Parse(args[0].Trim());

            if (!int.TryParse(args[0].Trim(), out int parsedProductId) || productId < 0)
                return "Invalid product id or product id is less than 0!";

            return _application.GetShoppingCartService().RemoveCartItem(clientContainer.GetClientId(), parsedProductId);
        }

        public string GetHelp()
        {
            return $"{GetName}([cartItemId])";
        }

        public string GetName()
        {
            return "RemoveCartItem";
        }
    }
}
