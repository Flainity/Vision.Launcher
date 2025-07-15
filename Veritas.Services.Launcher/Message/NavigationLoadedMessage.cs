using CommunityToolkit.Mvvm.Messaging.Messages;
using Veritas.Services.Launcher.Model;

namespace Veritas.Services.Launcher.Message;

public class NavigationLoadedMessage : ValueChangedMessage<List<NavigationItem>>
{
    public NavigationLoadedMessage(List<NavigationItem> value) : base(value)
    {
    }
}