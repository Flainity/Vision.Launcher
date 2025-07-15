using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Veritas.Services.Launcher.Message;

public class TitleChangedMessage : ValueChangedMessage<string>
{
    public TitleChangedMessage(string value) : base(value)
    {
    }
}