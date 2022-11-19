using System.Net;
using System.Net.Sockets;

namespace MessageBroker.Common;

public class Listener : IListener
{
    private TcpListener? server;
    private readonly string ipAddress;
    private readonly int port;

    public Listener(string ipAddress = "127.0.0.1", int port = 13000)
    {
        this.ipAddress = ipAddress;
        this.port = port;
    }

    public void Start(CancellationToken stoppingToken)
    {
        try
        {
            IPAddress localAddr = IPAddress.Parse(ipAddress);

            server = new TcpListener(localAddr, port);

            server.Start();

            // Buffer for reading data
            var bytes = new byte[256];
            string? data = null;


            while (!stoppingToken.IsCancellationRequested)
            {
                Console.Write("Waiting for a connection... ");

                // Perform a blocking call to accept requests.
                // You could also use server.AcceptSocket() here.
                using TcpClient client = server.AcceptTcpClient();
                Console.WriteLine("Connected!");

                data = null;

                // Get a stream object for reading and writing
                NetworkStream stream = client.GetStream();

                int i;

                // Loop to receive all the data sent by the client.
                while ((i = stream.Read(bytes, 0, bytes.Length)) != 0)
                {
                    // Translate data bytes to a ASCII string.
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i);
                    Console.WriteLine("Received: {0}", data);

                    // Process the data sent by the client.
                    data = data.ToUpper();

                    byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    // Send back a response.
                    stream.Write(msg, 0, msg.Length);
                    Console.WriteLine("Sent: {0}", data);
                }
            }
        }
        catch (SocketException e)
        {
            Console.WriteLine("SocketException: {0}", e);
        }
        finally
        {
            server?.Stop();
        }
    }
}
