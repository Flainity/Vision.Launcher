using System.Windows;
using Application.Launcher.Logging;
using Application.Launcher.Service.Client;
using Application.Launcher.Service.IO;
using Application.Launcher.Service.Settings;
using Application.Launcher.View;
using Application.Launcher.ViewModel;
using Application.Launcher.ViewModel.Windows;
using Application.Launcher.Windows;
using Microsoft.Extensions.DependencyInjection;
using Library.Configuration.Services;
using Library.Logging.Service;

namespace Application.Launcher;

public partial class App : System.Windows.Application
{
    public static IServiceProvider ServiceProvider { get; private set; }
    
    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var services = new ServiceCollection();

        services.AddSingleton<LoginWindow>();
        services.AddSingleton<LoginWindowViewModel>();

        services.AddScoped<MainWindow>();
        services.AddScoped<MainWindowViewModel>();

        services.AddSingleton<HomeView>();
        services.AddSingleton<HomeViewModel>();

        services.AddScoped<SettingsView>();
        services.AddScoped<SettingsViewModel>();

        services.AddSingleton<TopBarView>();
        services.AddSingleton<TopBarViewModel>();

        services.AddSingleton<PatchView>();
        services.AddSingleton<PatchViewModel>();

        services.AddSingleton<ISettingsReader, SettingsReader>();
        services.AddSingleton<IConfigurationService, ConfigurationService>();
        services.AddSingleton<IHttpClient, HttpClient>();
        services.AddSingleton<IVersionSystem, VersionSystem>();
        services.AddSingleton<IExtractService, ExtractService>();
        services.AddSingleton<ISocketService, SocketService>();

        services.AddKeyedSingleton<ILoggerService, VisualLoggerService>("visual");
        services.AddKeyedSingleton<ILoggerService, IOLoggerService>("file");

        ServiceProvider = services.BuildServiceProvider();

        var mainWindow = ServiceProvider.GetRequiredService<LoginWindow>();
        mainWindow.Show();
        ServiceProvider.GetRequiredService<MainWindow>().Show();
    }
}