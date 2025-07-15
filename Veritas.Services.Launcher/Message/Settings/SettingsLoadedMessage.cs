using CommunityToolkit.Mvvm.Messaging.Messages;
using Veritas.Library.McoSettings.Options;

namespace Veritas.Services.Launcher.Message.Settings;

public class SettingsLoadedMessage : ValueChangedMessage<IEnumerable<IGameOption>>
{
    public SettingsLoadedMessage(IEnumerable<IGameOption> value) : base(value)
    {
    }
}