using Application.Launcher.Message.Settings;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Library.McoSettings;
using Library.McoSettings.Options;

namespace Application.Launcher.ViewModel;

public partial class SettingsViewModel : ObservableObject, IRecipient<SettingsLoadedMessage>
{
    [ObservableProperty] private List<(int width, int height, int refreshRate)> _possibleResolutions;
    [ObservableProperty] private VideoOptions? _videoSettings;
    [ObservableProperty] private SoundOptions? _soundOptions;
    
    public SettingsViewModel()
    {
        WeakReferenceMessenger.Default.Register(this);
        PossibleResolutions = Resolutions.GetSupportedResolutions();
    }

    public void Receive(SettingsLoadedMessage message)
    {
        foreach (var gameOption in message.Value)
        {
            switch (gameOption)
            {
                case VideoOptions videoOptions:
                    VideoSettings = videoOptions;
                    break;
                case SoundOptions soundOptions:
                    SoundOptions = soundOptions;
                    break;
            }
        }
    }
}