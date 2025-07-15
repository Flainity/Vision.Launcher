namespace Veritas.Services.Launcher.Service.Client;

public interface ISocketService
{
    void Connected(IAsyncResult result);
    
    void SendLogin(string username, string password);
    void ReceiveLogin(IAsyncResult result);
}