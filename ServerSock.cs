using System;
using System.Net.Sockets;
using System.Net;
using System.IO;
public class ServerSocket
{
    public static void Main()
    {
        string externalip = new WebClient().DownloadString("http://icanhazip.com");
        bool ServerStatus = true;
        string ServerMessage = "";
        string ClientMessage =  "";
        Console.WriteLine ("Please Type in your Nickname");
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
                Console.WriteLine(Nickname+": ");
                ClientMessage = Nickname + ": " + Console.ReadLine();
                streamwrite.WriteLine(ClientMessage);
                streamwrite.Flush();
            }
        }
    }
}