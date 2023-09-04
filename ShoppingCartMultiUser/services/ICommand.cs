using ShoppingCartMultiUser.server;

namespace ShoppingCartMultiUser.services
{
    internal interface ICommand
    {
        string Execute(string[] args, ClientContainer clientContainer);
        string GetHelp();
        string GetName();
    }
}
