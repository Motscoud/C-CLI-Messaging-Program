using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
public class ServerSocket
{
    public static void Main()
    {
        Console.WriteLine("Which mode do you want to use?");
        Console.WriteLine("1 for Client Mode (Connect to another server");
        Console.WriteLine("2 for Server Mode (Host a connection for another user");
        int ModeSel = 0;
        string Mode = Console.ReadLine();
        try
        {
            ModeSel = int.Parse(Mode);
        }
        catch (FormatException)
        {
            Console.WriteLine("Only input numbers");
            Main();
        }
        ModeSel = Convert.ToInt32(Mode);
        if (ModeSel == 1)
            
                Client();
            
        else if (ModeSel == 2)
            
                Server();
            
        else
        Console.WriteLine("This mode does not exist.");
    }

    public static void Server()
    {
    Console.Clear();
    string externalip = new WebClient().DownloadString("http://icanhazip.com");

    bool ServerStatus = true;
    string ServerMessage = "";
    string ClientMessage = "";
    Console.WriteLine("Please Type in your Nickname");
        string Nickname = Console.ReadLine();
    //Initializes strings
    TcpListener tcpListen = new TcpListener(8700);
    tcpListen.Start();
        Console.WriteLine("Server is running!");
        Console.WriteLine("Please give your partner this IP Address: " + externalip);
        Socket socketForClient = tcpListen.AcceptSocket();
    Console.WriteLine("User Connected");
        NetworkStream netStream = new NetworkStream(socketForClient);
    StreamWriter streamwrite = new StreamWriter(netStream);
    StreamReader streamread = new StreamReader(netStream);

        while(ServerStatus)
        {
            if(socketForClient.Connected)
            {
                ServerMessage = streamread.ReadLine();
                Console.WriteLine(ServerMessage);
                Console.Write(Nickname+": ");
                ClientMessage = Nickname + ": " + Console.ReadLine();
                streamwrite.WriteLine(ClientMessage);
                streamwrite.Flush();
            }
        }
    }

    public static void Client()
    {
        Console.Clear();
        TcpClient socketForServer;
        bool ServerStatus = true;
        Console.WriteLine("Please type the Server Address");
        string ConnectIP = Console.ReadLine();
        Console.WriteLine("Please Type in your Nickname");
        string Nickname = Console.ReadLine();
        try
        {
            socketForServer = new TcpClient(ConnectIP, 8700);
            Console.WriteLine("Connected to Server!");
        }
        catch (System.Exception)
        {
            Console.WriteLine("Connection Failed, Please Double Check Your Server Address");
            return;
        }
        NetworkStream netStream = socketForServer.GetStream();
        StreamReader streamRead = new StreamReader(netStream);
        StreamWriter streamWrite = new StreamWriter(netStream);
        try
        {
            string ClientMessage = "";
            string ServerMessage = "";
            while (ServerStatus)
            {
                Console.Write(Nickname + ": ");
                ClientMessage = Console.ReadLine();
                if ((ClientMessage == "/exit"))
                {
                    ServerStatus = false;
                    streamWrite.WriteLine(Nickname + ": Has left the server");
                    streamWrite.Flush();
                }
                streamWrite.WriteLine(Nickname + ": " + ClientMessage);
                streamWrite.Flush();
                ServerMessage = streamRead.ReadLine();
                Console.WriteLine(ServerMessage);
            }

        }
        catch
        {
            Console.WriteLine("Error reading from Server! Has it went down?");
        }
        streamWrite.Close();
        netStream.Close();
        streamRead.Close();

    }
}
