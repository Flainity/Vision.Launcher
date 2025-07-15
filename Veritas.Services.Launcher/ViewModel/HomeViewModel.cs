using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Veritas.Services.Launcher.Message;
using Veritas.Services.Launcher.Service.Client;

namespace Veritas.Services.Launcher.ViewModel;

// TODO: Remove the login process from the HomeViewModel
public partial class HomeViewModel : ObservableObject, IRecipient<PatchProcessFinishedMessage>, IRecipient<LoginProcessFinishedMessage>
{
    [ObservableProperty] private bool _isPatchFinished = false;
    [ObservableProperty] private bool _isLoginProcess = false;
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;

    private ISocketService _socketService;

    public HomeViewModel(ISocketService socketService)
    {
        WeakReferenceMessenger.Default.RegisterAll(this);

        _socketService = socketService;
    }

    public void Receive(PatchProcessFinishedMessage message)
    {
        IsPatchFinished = message.Value;
    }

    public void Receive(LoginProcessFinishedMessage message)
    {
        IsLoginProcess = false;
    }

    [RelayCommand]
    private void SendLogin()
    {
        IsLoginProcess = true;
        WeakReferenceMessenger.Default.Send(new LoginProcessStartedMessage(true));
        _socketService.SendLogin(Username, Password);
    }
}