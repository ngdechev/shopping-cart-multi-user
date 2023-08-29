using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.admin
{
    [AllowedRoles(UserRole.Admin)]
    internal class DeleteProductCommand : ICommand
    {
        Application _application;

        public DeleteProductCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args)
        {
            if (args.Length < 1) 
                return $"Invalid number of arguments. Usage: {GetHelp()}";

            int productId = int.Parse(args[0].Trim());

            if(!int.TryParse(args[2].Trim(), out int parsedProductId) || productId < 0)
                return "Product ID cannot be smaller than 0!";

            return _application.GetDatabaseService().DeleteProduct(parsedProductId);
        }

        public string GetHelp()
        {
            return $"{GetName()}([productId])";
        }

        public string GetName()
        {
            return "DeleteProduct";
        }
    }
}
