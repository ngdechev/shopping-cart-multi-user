using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.none
{
    [AllowedRoles(UserRole.Admin, UserRole.Customer, UserRole.StorehouseWorker, UserRole.None)]
    internal class LoginCommand : ICommand
    {
        private Application _application;

        public LoginCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            UserRole role = (UserRole)Enum.Parse(typeof(UserRole), args[0]);

            if (string.IsNullOrEmpty(args[0]))
                return $"Please provide a valid user role! Usage: {GetHelp()}";

            return clientContainer.SetUserRole(role);
        }

        public string GetHelp()
        {
            return $"{GetName()}(UserRole)";
        }

        public string GetName()
        {
            return "Login";
        }
    }
}
