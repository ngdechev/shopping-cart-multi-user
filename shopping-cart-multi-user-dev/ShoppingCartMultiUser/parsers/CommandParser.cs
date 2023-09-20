using ShoppingCartMultiUser.commands;
using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using System.Reflection;

namespace ShoppingCartMultiUser
{
    internal static class CommandParser
    {
        private static Dictionary<string, ICommand> _commands = new Dictionary<string, ICommand>();

        public static string ParseCommands(string input, ClientContainer clientContainer)
        {
            string[] commandParts = input.Split('(');
            string commandName = commandParts[0], 
                arguments = commandParts[1].Trim('\n').TrimEnd(')'), 
                msg = string.Empty;

            foreach (KeyValuePair<string, ICommand> command in _commands)
            {
                if (command.Key == commandName)
                {
                    AllowedRolesAttribute? allowedRolesAttribute =
                            command.Value.GetType().GetCustomAttribute<AllowedRolesAttribute>();

                    if (allowedRolesAttribute == null || allowedRolesAttribute.Roles.Contains(clientContainer.GetUserRole()))
                    {
                        string[] args = arguments.Split(';');

                        msg = command.Value.Execute(args, clientContainer);
                        break;
                    }
                    else
                        msg = "You don't have permission to execute this command!";
                }
                else
                    msg = "Command not found!";
            }

            return msg;
        }

        public static Dictionary<string, ICommand> GetCommands()
        {
            return _commands;
        }

        public static void SetCommands(Dictionary<string, ICommand> commands)
        {
            _commands = commands;
        }
    }
}