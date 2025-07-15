using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Veritas.Services.Launcher.Message;
using Veritas.Services.Launcher.Model;

namespace Veritas.Services.Launcher.ViewModel;

public partial class SidebarViewModel : ObservableObject, IRecipient<NavigationLoadedMessage>
{
    [ObservableProperty] private List<NavigationItem>? _navigationItems;
    [ObservableProperty] private NavigationItem? _selectedItem;
    
    public SidebarViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        
        NavigationItems = new List<NavigationItem>
        {
            new("Home", NavigationAction.Home, "Home"),
            new("Settings", NavigationAction.Settings, "Cogs"),
        };
    }

    public void Receive(NavigationLoadedMessage message)
    {
        NavigationItems = message.Value;
    }

    partial void OnSelectedItemChanged(NavigationItem? oldValue, NavigationItem? newValue)
    {
        if (oldValue == newValue) return;
        if (newValue == null) return;
        
        WeakReferenceMessenger.Default.Send(new NavigationItemChangedMessage(newValue));
    }
}