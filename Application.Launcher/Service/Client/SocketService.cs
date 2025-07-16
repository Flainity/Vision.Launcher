using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Windows;
using Application.Launcher.Message;
using Application.Launcher.Type;
using CommunityToolkit.Mvvm.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Library.Logging.Service;

namespace Application.Launcher.Service.Client;

public class SocketService : ISocketService
{
    private readonly Socket _serverSocket;
    private byte[] _clientBuffer;

    private readonly ILoggerService _logger;
    
    public SocketService([FromKeyedServices("visual")] ILoggerService logger)
    {
        _logger = logger;
        _clientBuffer = new byte[1024];
        
        try
        {
            var ip = IPAddress.Parse("127.0.0.1");
            var endpoint = new IPEndPoint(ip, 5000);

            _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _serverSocket.BeginConnect(endpoint, Connected, _serverSocket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public void Connected(IAsyncResult result)
    {
        try
        {
            _serverSocket.EndConnect(result);
        }
        catch (SocketException e)
        {
            _logger.Error("Connection to the server has failed");
        }
    }

    public void SendLogin(string username, string password)
    {
        try
        {
            SendMessage($"{username}:{password}");

            _clientBuffer = new byte[1024];
            _serverSocket.BeginReceive(_clientBuffer, 0, _clientBuffer.Length, SocketFlags.None, ReceiveLogin,
                _serverSocket);
        }
        catch (SocketException e)
        {
            _logger.Error("Failed to connect to the server");
        }
    }

    public void ReceiveLogin(IAsyncResult result)
    {
        var length = _serverSocket.EndReceive(result);
        Array.Resize(ref _clientBuffer, length);
        var response = Encoding.ASCII.GetString(_clientBuffer);

        var tryParse = Enum.TryParse(response, out ServerResponseType responseType);
        
        if (!tryParse)
        {
            _logger.Error("Failed to parse the server response");
            return;
        }
        
        WeakReferenceMessenger.Default.Send(new LoginProcessFinishedMessage(responseType));
    }

    private void SendMessage(string message)
    {
        try
        {
            var buffer = Encoding.ASCII.GetBytes(message);
            _serverSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, MessageSent, _serverSocket);
        }
        catch (SocketException e)
        {
            _logger.Error("Failed to connect to the server");
        }
    }

    private void MessageSent(IAsyncResult result)
    {
        _serverSocket.EndSend(result);
    }
}