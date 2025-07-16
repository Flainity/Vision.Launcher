using System.Windows.Controls;
using Application.Launcher.Message.Settings;
using Application.Launcher.Service.Settings;
using Application.Launcher.ViewModel;
using CommunityToolkit.Mvvm.Messaging;
using Library.McoSettings.Options;
using Application.Launcher.Model.Settings;

namespace Application.Launcher.View;

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