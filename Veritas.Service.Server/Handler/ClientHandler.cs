using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Veritas.Service.Server.Handler;

public class ClientHandler
{
    private Socket clientSocket;
    private IPEndPoint clientEndPoint;
    private byte[] clientBuffer;

    public ClientHandler(Socket handleSocket)
    {
        try
        {
            clientSocket = handleSocket;
            clientEndPoint = (IPEndPoint)handleSocket.RemoteEndPoint;
            clientBuffer = new byte[1024];

            clientSocket.BeginReceive(clientBuffer, 0, clientBuffer.Length, SocketFlags.None, ReceiveLogin,
                clientSocket);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private void ReceiveLogin(IAsyncResult result)
    {
        try
        {
            var receivedLength = clientSocket.EndReceive(result);
            Array.Resize(ref clientBuffer, receivedLength);

            if (receivedLength != 0)
            {
                var decryptedMessage = Encoding.ASCII.GetString(clientBuffer);
                Console.WriteLine($"Received Message from client: {decryptedMessage}");
                
                Send($"You are pretty cool!");

                clientBuffer = new byte[1024];
                clientSocket.BeginReceive(clientBuffer, 0, clientBuffer.Length, SocketFlags.None, ReceiveLogin,
                    clientSocket);
            }
        }
        catch (Exception e)
        {
            Send($"Failure#Socket Failure: {e.Message}, {e.Source}, {e.StackTrace}");
        }
    }

    private static bool StringContainsInvalidChars(string value)
    {
        return value.Any(character => !char.IsLetterOrDigit(character));
    }

    private void Send(string message)
    {
        try
        {
            var buffer = Encoding.ASCII.GetBytes(message);
            clientSocket.BeginSend(buffer, 0, buffer.Length, SocketFlags.None, MessageSent, clientSocket);
        }
        catch
        {
            // ignored
        }
    }

    private void MessageSent(IAsyncResult result)
    {
        try
        {
            clientSocket.EndSend(result);
        }
        catch
        {
            // ignored
        }
    }
}