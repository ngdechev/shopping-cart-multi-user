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

        private volatile bool _isRunning; // direktno pishe v promenlivata
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

            //stop()
            lock (_clientsLock)
            {
                if (_connectedClients.Count == 0)
                    _connectedClients.Clear();
                else
                {
                    foreach (ClientHandler client in _connectedClients)
                        client.Disconnect();
                }
            }

            _application.GetDatabaseService().Cleanup();

            // check for null
            if (_listener != null)
                _listener?.Stop();
            
            // for loop
            foreach (ClientHandler client in _connectedClients)
                client.SendMessage("Server stopped! All the clients are disconnected!");
        }

        public void Stop()
        {
            _isRunning = false;
        }

        public void RemoveClient(ClientHandler client)
        {
            lock (_clientsLock)
            {
                _connectedClients.Remove(client);
            }
        }
    }
}
