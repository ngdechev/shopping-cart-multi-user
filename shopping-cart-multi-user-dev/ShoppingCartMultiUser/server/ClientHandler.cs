using ShoppingCartMultiUser.server;
using System.Net.Sockets;
using System.Text;

namespace ShoppingCartMultiUser
{
    class ClientHandler
    {
        // user role tuk 
        private TcpClient _clientSocket;
        private NetworkStream _stream;
        private ServerHandler _server;
        private Application _application;
        private int _clientId;

        public ClientHandler(TcpClient clientSocket, ServerHandler server, Application application, int clientId) // da dobavq app
        {
            _clientSocket = clientSocket;
            _server = server;
            _clientId = clientId;
            _application = server._application;
            _stream = clientSocket.GetStream();

            _application.GetShoppingCartService().AddNewClientContainer(new ClientContainer(clientId));
        }

        public void HandleClient()
        {
            while(true)
            {
                byte[] buffer = new byte[1024];
                int bytesRead;
                
                try { 
                    while((bytesRead = _stream.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead), response = string.Empty;
                        string[] lines = message.Split('\n');

                        Console.WriteLine($"Received: {message}");

                        if (message != null) // koga shte vyrne null?
                        { 
                            if (lines.Length > 1) // ako ne e cql red?
                            {
                                for (int i = 0; i < lines.Length - 1; i++)
                                {
                                    string line = lines[i].Trim('\r');

                                    Console.WriteLine("Received: " + line);

                                    // execute tuk
                                    response = CommandParser.ParseCommands(line, _application.GetShoppingCartService().GetClientContainer(_clientId));
                                }
                            }
                        }

                        SendMessage(response);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);

                    //Disconnect();
                }
            }

        }

        public void SendMessage(string message)
        {
            lock (this)
            {
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);

                _stream.Write(messageBytes, 0, messageBytes.Length);
                _stream.Flush();
            }
        }

        public void Disconnect()
        {
            Console.WriteLine("Client disconnected.");

            _server.RemoveClient(this);

            try
            {
                _stream.Close();
                _clientSocket.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during client disconnection: {ex.Message}");
            }
        }
    }
}