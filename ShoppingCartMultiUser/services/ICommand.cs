namespace ShoppingCartMultiUser.services
{
    internal interface ICommand
    {
        string Execute(string[] args);
        string GetHelp();
        string GetName();
    }
}
