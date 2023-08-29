using ShoppingCartMultiUser.services;
using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.commands.admin
{
    [AllowedRoles(UserRole.Admin, UserRole.Customer, UserRole.StorehouseWorker, UserRole.None)]
    internal class ExitCommand : ICommand
    {
        Application _application;

        public ExitCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args)
        {
            _application.Exit();
            return "Bye!";
        }

        public string GetHelp()
        {
            return $"{GetName()}()";
        }

        public string GetName()
        {
            return "Exit";
        }
    }
}
