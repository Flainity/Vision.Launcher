using Application.Launcher.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Application.Launcher.Message;

public class NavigationLoadedMessage : ValueChangedMessage<List<NavigationItem>>
{
    public NavigationLoadedMessage(List<NavigationItem> value) : base(value)
    {
    }
}