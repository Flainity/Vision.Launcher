using Application.Launcher.Model;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Application.Launcher.Message;

public class NavigationItemChangedMessage : ValueChangedMessage<NavigationItem>
{
    public NavigationItemChangedMessage(NavigationItem value) : base(value)
    {
    }
}