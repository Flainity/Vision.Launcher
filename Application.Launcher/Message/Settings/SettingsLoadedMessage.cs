using CommunityToolkit.Mvvm.Messaging.Messages;
using Library.McoSettings.Options;

namespace Application.Launcher.Message.Settings;

public class SettingsLoadedMessage : ValueChangedMessage<IEnumerable<IGameOption>>
{
    public SettingsLoadedMessage(IEnumerable<IGameOption> value) : base(value)
    {
    }
}