using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;

internal class Program {
    TcpClient client = new TcpClient();

    private async Task ServerConnect(){
        try
        {
            
            IPAddress IpAdres = IPAddress.Parse("127.0.0.9");
            int port = 5000;

            await client.ConnectAsync(IpAdres, port);
            HandleClient(client);
        }
        catch (Exception er)
        {
            Console.WriteLine(er.Message);
        }
        finally {
            client.Close();
        }
    }

    private static async void HandleClient(TcpClient client) 
    {
        NetworkStream stream = client.GetStream();
        var reader = new StreamReader(stream, Encoding.UTF8);
        var writer = new StreamWriter(stream, new UTF8Encoding(false)) { AutoFlush = true };

        try
        {
            string message;
            while ((message = await reader.ReadLineAsync()) != null)
            {
                Console.WriteLine(message);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    //public void ResetPuli() {
    //    Console.WriteLine("cos zrobilem");
    
    //}



    static async Task Main(string[] args)
    {
        Program p = new Program();
        await p.ServerConnect();
        Console.WriteLine("Witam w panelu admina do gry");
        //while (true) {
        //    Console.WriteLine($"To co mozesz wykonac: 1:REST,2:SET");
        //    int wybor = Convert.ToInt32(Console.ReadLine());
        //    switch (wybor) {
        //        case 1: {
        //                p.ResetPuli();
        //                break;   
        //         }
            
        //    }
        //}

    }
}