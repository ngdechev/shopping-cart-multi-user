using System.Net.Sockets;
using System.Text;

namespace ShoppingCartMultiUser
{
    class ClientHandler
    {
        private TcpClient _clientSocket;
        private NetworkStream _stream;
        private ServerHandler _server;
        private Application _application;
        private int _id;

        public ClientHandler(TcpClient clientSocket, ServerHandler server, int clientId)
        {
            _clientSocket = clientSocket;
            _server = server;
            _id = clientId;

            _application = new();
            _stream = clientSocket.GetStream();
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
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Received: {message}");

                        string[] lines = message.Split('\n');

                        if (message != null) { 
                        
                            if (lines.Length > 1)
                            {
                                for (int i = 0; i < lines.Length - 1; i++)
                                {
                                    string line = lines[i].Trim('\r');
                                    Console.WriteLine("Received: " + line);

                                    CommandParser.ParseCommands(_application.GetRole(), line);
                                }
                            }
                        }

                        string response = _application.Run(message);
                        SendMessage(response);

                        _server.BroadcastMessage(response, this);
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
            byte[] messageBytes = Encoding.ASCII.GetBytes(message);

            _stream.Write(messageBytes, 0, messageBytes.Length);
            _stream.Flush();
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