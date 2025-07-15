using System.Windows.Controls;
using CommunityToolkit.Mvvm.Messaging;
using Veritas.Library.McoSettings.Options;
using Veritas.Services.Launcher.Message.Settings;
using Veritas.Services.Launcher.Model.Settings;
using Veritas.Services.Launcher.Service.Settings;
using Veritas.Services.Launcher.ViewModel;

namespace Veritas.Services.Launcher.View;

public partial class SettingsView : UserControl
{
    public SettingsViewModel ViewModel => (SettingsViewModel)DataContext;
    private ISettingsReader SettingsService { get; }
    
    public SettingsView(SettingsViewModel viewModel, ISettingsReader settingsService)
    {
        InitializeComponent();
        
        DataContext = viewModel;
        SettingsService = settingsService;
        
        WeakReferenceMessenger.Default.Send(new SettingsLoadedMessage(new List<IGameOption>()));
        
        LoadSettingsAsync();
    }
    
    private async void LoadSettingsAsync()
    {
        var settings = await SettingsService.Read();
        WeakReferenceMessenger.Default.Send(new SettingsLoadedMessage(settings));
    }
}