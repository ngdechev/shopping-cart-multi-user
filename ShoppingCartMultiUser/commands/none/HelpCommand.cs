using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using System.Reflection;
using System.Text;

namespace ShoppingCartMultiUser.commands.none
{
    internal class HelpCommand : ICommand
    {
        Application _application;

        public HelpCommand(Application application)
        {
            _application = application;
        }

        public string Execute(string[] args, ClientContainer clientContainer)
        {
            StringBuilder helpMessage = new("Available commands for your role:\n");

            foreach (var kvp in CommandParser.GetCommands())
            {
                var command = kvp.Value;
                var allowedRolesAttribute = command.GetType().GetCustomAttribute<AllowedRolesAttribute>();

                if (allowedRolesAttribute != null && allowedRolesAttribute.Roles.Contains(clientContainer.GetUserRole()))
                    helpMessage.AppendLine($"[{kvp.Key}]: {command.GetHelp()}");
            }

            return helpMessage.ToString();
        }

        public string GetHelp()
        {
            return $"{GetName()}()";
        }

        public string GetName()
        {
            return "Help";
        }
    }
}
