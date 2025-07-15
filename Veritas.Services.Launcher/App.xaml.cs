using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Veritas.Library.Configuration.Services;
using Veritas.Library.Logging.Service;
using Veritas.Services.Launcher.Logging;
using Veritas.Services.Launcher.Service.Client;
using Veritas.Services.Launcher.Service.IO;
using Veritas.Services.Launcher.Service.Settings;
using Veritas.Services.Launcher.View;
using Veritas.Services.Launcher.ViewModel;
using Veritas.Services.Launcher.ViewModel.Windows;
using Veritas.Services.Launcher.Windows;

namespace Veritas.Services.Launcher;

public partial class App : Application
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
    }
}