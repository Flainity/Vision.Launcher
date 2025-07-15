using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using Veritas.Library.Database.Context;
using Veritas.Library.Database.Schema.Account;
using Veritas.Service.Server.Encryption;
using Veritas.Service.Server.Localization;

namespace Veritas.Service.Server.Services;

public class SocketService
{
    private readonly TcpListener _listener = new(IPAddress.Any, 5000);
    private readonly AccountContext _accountContext;
    private readonly HashGenerator _hashGenerator;

    public SocketService(AccountContext accountContext, HashGenerator hashGenerator)
    {
        _accountContext = accountContext;
        _hashGenerator = hashGenerator;
    }

    public async Task StartAsync()
    {
        _listener.Start();
        Console.WriteLine(Resources.Socket_Started, (_listener.LocalEndpoint as IPEndPoint)?.Port);

        while (true)
        {
            var client = await _listener.AcceptTcpClientAsync();
            var clientIpEndPoint = client.Client.RemoteEndPoint as IPEndPoint;
            
            Console.WriteLine(Resources.Socket_ClientConnected, clientIpEndPoint?.Address);

            _ = HandleClientAsync(client);
        }
    }

    private async Task HandleClientAsync(TcpClient client)
    {
        var stream = client.GetStream();
        var buffer = new byte[1024];

        while (true)
        {
            int bytesRead;
            try
            {
                bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                
                if (bytesRead == 0)
                {
                    Console.WriteLine(Resources.Socket_ClientDisconnect, (client.Client.RemoteEndPoint as IPEndPoint)?.Address);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(Resources.Socket_ClientDisconnectException, (client.Client.RemoteEndPoint as IPEndPoint)?.Address, e.Message);
                break;
            }

            var message = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            var split = message.Split(':', 2);

            var username = split[0].Trim();
            var password = _hashGenerator.CreateMD5(split[1].Trim(), true);

            var user = await _accountContext.Users.FirstOrDefaultAsync(u => u.Username == username && u.Password == password);
            var responseBytes = Array.Empty<byte>();

            if (user is User)
            {
                var response = "0";
                responseBytes = Encoding.UTF8.GetBytes(response);
            }
            else
            {
                responseBytes = "1"u8.ToArray();
            }

            await stream.WriteAsync(responseBytes, 0, responseBytes.Length);
        }

        client.Close();
    }
}