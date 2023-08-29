using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.admin
{
    [AllowedRoles(UserRole.Admin)]
    internal class EditProductCommand : ICommand
    {
        public string Execute(string[] args)
        {
            throw new NotImplementedException();
        }

        public string GetHelp()
        {
            return $"{GetName()}([productId])";
        }

        public string GetName()
        {
            return "EditProduct";
        }
    }
}
