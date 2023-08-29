using ShoppingCartMultiUser;

ServerHandler server = new();

Console.CancelKeyPress += (sender, e) =>
{
    e.Cancel = true;

    server.Stop();
};

server.Start();