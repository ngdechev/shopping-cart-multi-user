using ShoppingCartMultiUser.utils;

namespace ShoppingCartMultiUser.services
{
    internal interface IApplication
    {
        string Run(string input);
        void Exit();
        IDatabaseService GetDatabaseService();
        IShoppingCartService GetShoppingCartService();

        //List<CommandItem> GetCommands();

        UserRole GetRole();
        string SetRole(UserRole role);

        void PrintMessage(string msg);
    }
}
