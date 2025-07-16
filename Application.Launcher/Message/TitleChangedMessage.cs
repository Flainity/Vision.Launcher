using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Application.Launcher.Message;

public class TitleChangedMessage : ValueChangedMessage<string>
{
    public TitleChangedMessage(string value) : base(value)
    {
    }
}