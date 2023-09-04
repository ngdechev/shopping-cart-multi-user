using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.admin
{
    [AllowedRoles(UserRole.Admin)]
    internal class EditProductCommand : ICommand
    {
        private Application _application;

        public EditProductCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            if (args.Length == 3)
                return $"Invalid number of arguments. Usage: {GetHelp()}";
            
            int productId = int.Parse(args[0].Trim());
            string productField = args[1].Trim();
            string newValue = args[1].Trim();

            if (!int.TryParse(args[0].Trim(), out int parsedProductId) || parsedProductId < 0)
                return "Product ID cannot be empty!";
            if (string.IsNullOrEmpty(productField))
                return "Product field to edit cannot be empty!";
            if (string.IsNullOrEmpty(newValue))
                return "New field value cannot be empty!";

            return _application.GetDatabaseService().UpdateProduct(parsedProductId, productField, newValue);
        }
        public string GetHelp()
        {
            return $"{GetName()}([productId], \"fieldToEdit\")";
        }

        public string GetName()
        {
            return "EditProduct";
        }
    }
}
