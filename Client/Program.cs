using System;
using System.Net.Sockets;
using System.Text;

namespace TCPClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            TcpClient client = new TcpClient();
            //client.Connect("localhost", 12345); // Replace with the server's IP and port
            client.Connect("172.20.80.37", 12345);
            NetworkStream stream = client.GetStream();
            byte[] buffer = new byte[1024];
            bool run = true;

            Console.WriteLine("Connected to the server. Type 'exit' to quit.");
            Console.CancelKeyPress += (sender, e) =>
            {
                stream.Close();
                client.Close();

                Console.WriteLine("Disconnected from the server.");
                //run = false;
            };
            // Start a separate thread to receive messages from the server
            Task.Run(() => ReceiveMessages(stream));

            while (true)//run)
            {
                string message = Console.ReadLine();

                if (message == "exit")
                {
                    break;
                }

                message = message + "\n";
                byte[] messageBytes = Encoding.ASCII.GetBytes(message);
                stream.Write(messageBytes, 0, messageBytes.Length);
            }

            // Close the stream and client
            stream.Close();
            client.Close();
            Console.WriteLine("Disconnected from the server.");
        }

        static void ReceiveMessages(NetworkStream stream)
        {
            byte[] buffer = new byte[1024];

            while (true)
            {
                try
                {
                    int bytesRead = stream.Read(buffer, 0, buffer.Length);
                    if (bytesRead == 0)
                    {
                        // If no bytes are read, it means the server has closed the connection
                        Console.WriteLine("bye!");
                        break;
                    }
                    else
                    {
                        string message = Encoding.ASCII.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"Server: {message}");
                        bytesRead = 0;
                    }

                }
                catch (Exception ex)
                {
                    // An exception will be thrown if the server closes the connection abruptly
                    Console.WriteLine(ex.Message);
                    break;
                }
            }
        }
    }
}