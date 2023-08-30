using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using System.Net;
using System.Net.Sockets;

namespace ShoppingCartMultiUser
{
    internal class ServerHandler
    {
        private TcpListener? _listener;
        private List<ClientHandler> _connectedClients = new();
        private readonly object _clientsLock = new();
        private bool _isRunning = true;
        private int _clientId = 0;
        private ClientContainer _clientContainer;
        private Application _application;

        Dictionary<int, ShoppingCartService> clientShoppingCarts = new Dictionary<int, ShoppingCartService>();


        public void Start()
        {  
            _listener = new(IPAddress.Any, 12345);
            _listener.Start();

            Console.WriteLine("Server started, waiting for clients...");

            _application = new();

            while (_isRunning)
            {
                if (_listener.Pending())
                {
                    TcpClient clientSocket = _listener.AcceptTcpClient();

                    Console.WriteLine("Client connected!");

                    ClientHandler _clientHandler = new(clientSocket, this, _clientId);

                    lock (_clientsLock)
                    {
                        _connectedClients.Add(_clientHandler);
                        _clientContainer = new(_application);

                        ShoppingCartService newCart = new(_application, _clientId);
                        clientShoppingCarts[_clientId] = newCart;

                        _clientId++;
                        //_shoppingCartMap.Add(_clientId, )
                    }
                        

                    Thread clientThread = new Thread(_clientHandler.HandleClient);
                    clientThread.Start();
                }

                Thread.Sleep(10);
            }
        }

        public void Stop()
        {
            lock (_clientsLock)
            {
                if(_connectedClients.Count == 0)
                    _connectedClients.Clear();
                else
                {
                    foreach (ClientHandler client in _connectedClients)
                        client.Disconnect();        
                }
            }

            _listener?.Stop();

            Console.WriteLine("Server stopped! All the clients are disconnected!");

            _isRunning = false;
        }

        public void BroadcastMessage(string message, ClientHandler senderClient)
        {
            lock (_clientsLock)
                foreach (ClientHandler client in _connectedClients)
                    if (client != senderClient)
                        client.SendMessage(message);
        }

        public void RemoveClient(ClientHandler client)
        {
            lock (_clientsLock)
                _connectedClients.Remove(client);
        }
    }
}
