using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    TcpClient client = new TcpClient();

    private async Task ServerConnect()
    {
        try
        {
           
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            await client.ConnectAsync(ipAddress, port);
            Console.WriteLine("Połączono z serwerem.");

            await HandleClient(client);
        }
        catch (Exception er)
        {
            Console.WriteLine(er.Message);
        }
        finally
        {
            client.Close();
        }
    }

    private static async Task HandleClient(TcpClient client)
    {
        NetworkStream stream = client.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);
        var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

        
        var receiveTask = Task.Run(async () =>
        {
            try
            {
                string message;
                while ((message = await reader.ReadLineAsync()) != null)
                {
                    Console.WriteLine($"Serwer: {message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd odbioru: " + ex.Message);
            }
        });

        
        try
        {
            string send;
            while ((send = Console.ReadLine()) != null)
            {
                await writer.WriteLineAsync(send);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Błąd wysyłania: " + ex.Message);
        }
    }

    static async Task Main(string[] args)
    {
        Program p = new Program();
        await p.ServerConnect();
        Console.WriteLine("Panel admina do gry – wpisuj komendy lub wiadomości:");
    }
}