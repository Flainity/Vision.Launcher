using Application.Launcher.Message;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace Application.Launcher.ViewModel;

public partial class TopBarViewModel : ObservableObject, IRecipient<TitleChangedMessage>
{
    [ObservableProperty]
    private string _title = "Fiesta Online";

    public TopBarViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
    }

    public void Receive(TitleChangedMessage message)
    {
        Title = message.Value;
    }

    [RelayCommand]
    private static void Exit()
    {
        Environment.Exit(1);
    }
}