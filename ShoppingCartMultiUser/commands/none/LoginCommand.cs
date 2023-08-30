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

        public string Execute(string[] args)
        {
            UserRole role = (UserRole)Enum.Parse(typeof(UserRole), args[0]);

            if (string.IsNullOrEmpty(args[0]))
                return $"Please provide a valid user role! Usage: {GetHelp()}";

            //if (!_application.GetUserRoles().Contains(role))
            //  return $"The role {role} does not exist!";

            return _application.SetRole(role);
        }

        public string GetHelp()
        {
            return $"{GetName()}(Role)";
        }

        public string GetName()
        {
            return "Login";
        }
    }
}
