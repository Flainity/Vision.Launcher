using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Application.Launcher.Message;

public class PatchProcessFinishedMessage(bool value) : ValueChangedMessage<bool>(value);