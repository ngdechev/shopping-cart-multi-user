using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.customer
{
    [AllowedRoles(UserRole.Customer)]
    internal class AddCartItemCommand : ICommand
    {
        Application _application;

        public AddCartItemCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            if (args.Length < 2) 
                return $"Invalid number of arguments. Usage: {GetHelp()}";

            int productId = int.Parse(args[0].Trim());
            int productQuantity = int.Parse(args[1].Trim());

            if (!int.TryParse(args[0].Trim(), out int parsedProductId) || productId < 0)
                return "Invalid product id or product id is less than 0!";

            if (!int.TryParse(args[1].Trim(), out int parsedProductQuantity) || productQuantity < 0)
                return "Invalid quantity or quantity is less than 0!";

            return _application.GetShoppingCartService().AddCartItem(clientContainer.GetClientId(),parsedProductId, parsedProductQuantity);
        }

        public string GetHelp()
        {
            return $"{GetName()}([productId]; [quantity])";
        }

        public string GetName()
        {
            return "AddCartItem";
        }
    }
}
