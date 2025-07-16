using Application.Launcher.Message;
using Application.Launcher.Model;
using Application.Launcher.View;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Application.Launcher.ViewModel;

public partial class MainWindowViewModel : ObservableObject, IRecipient<NavigationItemChangedMessage>
{
    [ObservableProperty] private object? _currentView;
    [ObservableProperty] private PatchView? _patchView;

    private HomeView? _homeView;
    private SettingsView? _settingsView;

    public MainWindowViewModel(HomeView homeView, SettingsView settingsView, PatchView patchView)
    {
        WeakReferenceMessenger.Default.Register(this);

        _homeView = homeView;
        _settingsView = settingsView;
        _patchView = patchView;

        CurrentView = _homeView;
    }

    public void Receive(NavigationItemChangedMessage message)
    {
        switch (message.Value.Action)
        {
            case NavigationAction.Settings:
                CurrentView = _settingsView;
                break;
            case NavigationAction.Home:
                CurrentView = _homeView;
                break;
            default:
                break;
        }
    }
}