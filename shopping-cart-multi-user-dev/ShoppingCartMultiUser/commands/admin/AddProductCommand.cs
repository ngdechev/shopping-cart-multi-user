using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.admin
{
    [AllowedRoles(UserRole.Admin)]
    internal class AddProductCommand : ICommand
    {
        //Logger.Log("debug", "AddProductCommandClass");

        Application _application;

        public AddProductCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            if (args.Length < 4) 
                Console.WriteLine($"Invalid number of arguments. Usage: {GetHelp()}");

            string productName = args[0].Trim();
            float productPrice = float.Parse(args[1].Trim());
            int productQuantity = int.Parse(args[2].Trim());
            string productDescription = args[3].Trim();

            if (string.IsNullOrEmpty(productName))
                return "Product name cannot be empty!";

            if (!float.TryParse(args[1].Trim(), out float parsedProductPrice) || parsedProductPrice < 0)
                return "Invalid product price format or price cannot be less than 0!";

            if (!int.TryParse(args[2].Trim(), out int parsedProductQuantity) || parsedProductQuantity < 0)
                return "Invalid product quantity format or quantity cannot be less than 0!";

            if (string.IsNullOrEmpty(productDescription))
                return "Product description cannot be empty!";

            return _application.GetDatabaseService().AddProduct(productName, parsedProductPrice, parsedProductQuantity, productDescription);
        }

        public string GetHelp()
        {
            return $"{GetName()}(\"productName\"; [productPrice]; [productQuantity]; \"productDescription\")";
        }

        public string GetName()
        {
            return "AddProduct";
        }
    }
}
