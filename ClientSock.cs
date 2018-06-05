using System;
using System.Net.Sockets;
using System.IO;
public class ClientSocket
{
    static void Main(string[] args)
    {
        TcpClient socketForServer;
        bool ServerStatus = true;
        Console.WriteLine ("Please type the Server Address");
        string ConnectIP = Console.ReadLine();
        Console.WriteLine ("Please Type in your Nickname");
        string Nickname = Console.ReadLine();
        try
        {
            socketForServer = new TcpClient(ConnectIP,8700);
            Console.WriteLine("Connected to Server!");
        }
        catch (System.Exception)
        {
            Console.WriteLine ("Connection Failed, Please Double Check Your Server Address");
            return;
        }
        NetworkStream netStream = socketForServer.GetStream();
        StreamReader streamRead = new StreamReader(netStream);
        StreamWriter streamWrite = new StreamWriter(netStream);
        try
        {
            string ClientMessage = "";
            string ServerMessage = "";
            while(ServerStatus)
            {
                Console.WriteLine(Nickname+": ");
                ClientMessage = Console.ReadLine();
                if((ClientMessage=="/exit"))
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