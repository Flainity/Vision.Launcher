using System.Windows;
using Veritas.Library.Logging.Service;

namespace Veritas.Services.Launcher.Logging;

public class VisualLoggerService : ILoggerService
{
    public void Debug(string message) => Log(message, "Debug", MessageBoxImage.Question);
    public void Info(string message) => Log(message, "Info", MessageBoxImage.Information);
    public void Success(string message) => Log(message, "Success", MessageBoxImage.Information);
    public void Warning(string message) => Log(message, "Warning", MessageBoxImage.Warning);
    public void Error(string message) => Log(message, "Error", MessageBoxImage.Error);

    private void Log(string message, string title, MessageBoxImage icon) => MessageBox.Show(message, title, MessageBoxButton.OK, icon);
}