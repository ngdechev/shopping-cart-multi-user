using ShoppingCartMultiUser.entity;
using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;

namespace ShoppingCartMultiUser.commands.other
{
    internal class UpdateProductQuantity : ICommand
    {
        private Application _application;

        public UpdateProductQuantity(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            if (!int.TryParse(args[0].Trim(), out int parsedProductId) || parsedProductId < 0)
                return "Product ID cannot be empty!";
            if (!int.TryParse(args[1].Trim(), out int parsedProductQuantity) || parsedProductQuantity < 0)
                return "Product quantity cannot be empty!";

            return _application.GetDatabaseService().UpdateProductQuantity(parsedProductId, parsedProductQuantity);
        }

        public string GetHelp()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }
    }
}
