using ShoppingCartMultiUser.server;
using ShoppingCartMultiUser.services;
using System.Net;
using System.Net.Sockets;

namespace ShoppingCartMultiUser
{
    internal class ServerHandler
    {
        public readonly object _clientsLock;
        
        private TcpListener? _listener;
        private ClientHandler _clientHandler;
        public Application _application;
        private List<ClientHandler> _connectedClients;
        private ProductDatabaseService _productDatabaseService;

        private bool _isRunning;
        private int _clientId;

        public ServerHandler()
        {
            _clientId = 0;

            _connectedClients = new();
            _clientsLock = new();
            _isRunning = true;
            _application = new();
            _productDatabaseService = new(_application);
            
            _productDatabaseService.Init();
        }

        public void Start(int serverPort)
        {  
            _listener = new(IPAddress.Any, serverPort);
            _listener.Start();

            Console.WriteLine("Server started, waiting for clients...");

            while (_isRunning)
            {
                if (_listener.Pending())
                {
                    TcpClient clientSocket = _listener.AcceptTcpClient();

                    Console.WriteLine("Client connected!");

                    _clientHandler = new(clientSocket, this, _clientId);

                    lock (_clientsLock)
                    {
                        _clientId++;
                        _connectedClients.Add(_clientHandler);
                    }

                    Thread clientThread = new(_clientHandler.HandleClient);

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

            _application.GetDatabaseService().Cleanup();

            _listener?.Stop();

            _clientHandler.BroadcastMessage("Server stopped! All the clients are disconnected!");

            _isRunning = false;
        }

        public void RemoveClient(ClientHandler client)
        {
            lock (_clientsLock)
                _connectedClients.Remove(client);
        }

        public int GetClientId()
        {
            return _clientId;
        }
    }
}
