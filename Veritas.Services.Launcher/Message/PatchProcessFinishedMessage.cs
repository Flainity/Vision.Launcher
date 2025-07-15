using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Veritas.Services.Launcher.Message;

public class PatchProcessFinishedMessage(bool value) : ValueChangedMessage<bool>(value);