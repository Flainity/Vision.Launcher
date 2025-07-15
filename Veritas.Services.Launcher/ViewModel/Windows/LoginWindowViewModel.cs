using System.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Veritas.Services.Launcher.Message;
using Veritas.Services.Launcher.Service.Client;
using Microsoft.Extensions.DependencyInjection;
using Veritas.Services.Launcher.Windows;
using Veritas.Library.Logging.Service;
using Veritas.Services.Launcher.Type;

namespace Veritas.Services.Launcher.ViewModel.Windows;

public partial class LoginWindowViewModel : ObservableObject, IRecipient<LoginProcessFinishedMessage>
{
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;
    [ObservableProperty] private bool _isLoginProcessRunning = false;
    
    private readonly ISocketService _socketService;
    private readonly ILoggerService _loggerService;

    public LoginWindowViewModel(ISocketService socketService, [FromKeyedServices("visual")] ILoggerService logger)
    {
        _socketService = socketService;
        _loggerService = logger;

        WeakReferenceMessenger.Default.RegisterAll(this);
    }

    [RelayCommand]
    private void RequestLogin()
    {
        IsLoginProcessRunning = true;
        _socketService.SendLogin(Username, Password);
    }

    public void Receive(LoginProcessFinishedMessage message)
    {
        IsLoginProcessRunning = false;

        switch (message.Value)
        {
            case ServerResponseType.Success:
                AuthenticationSuccess();
                break;
            case ServerResponseType.WrongCredentials:
                _loggerService.Error("Login failed: Wrong credentials.");
                break;
            case ServerResponseType.Error:
                _loggerService.Error("Login failed: An error occurred during the login process.");
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private static void AuthenticationSuccess()
    {
        Application.Current.Dispatcher.InvokeAsync(() =>
        {
            var loginWindow = App.ServiceProvider.GetRequiredService<LoginWindow>();
            var mainWindow = App.ServiceProvider.GetRequiredService<MainWindow>();

            mainWindow.Show();
            loginWindow.Close();
        });
    }
}
