using System.Collections.ObjectModel;
using Application.Launcher.Message;
using Application.Launcher.Model;
using Application.Launcher.Service.Client;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Application.Launcher.ViewModel;

// TODO: Remove the login process from the HomeViewModel
public partial class HomeViewModel : ObservableObject, IRecipient<PatchProcessFinishedMessage>,
    IRecipient<LoginProcessFinishedMessage>
{
    [ObservableProperty] private bool _isPatchFinished = false;
    [ObservableProperty] private bool _isLoginProcess = false;
    [ObservableProperty] private string _username = string.Empty;
    [ObservableProperty] private string _password = string.Empty;

    [ObservableProperty] private ObservableCollection<NewsItem> _newsItems = new ObservableCollection<NewsItem>
    {
        new NewsItem
            { Title = "Welcome to Vision of the Past", Content = "This is the first news item.", Tag = "General" },
        new NewsItem
        {
            Title = "Patch 1.0 Released",
            Content =
                "The first patch has been released with bug fixes and improvements. Ich sag euch, das ist alles der Wahnsinn hier.",
            Tag = "Patch"
        }
    };

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