using CommunityToolkit.Mvvm.Messaging.Messages;
using Veritas.Services.Launcher.Model;

namespace Veritas.Services.Launcher.Message;

public class NavigationItemChangedMessage : ValueChangedMessage<NavigationItem>
{
    public NavigationItemChangedMessage(NavigationItem value) : base(value)
    {
    }
}