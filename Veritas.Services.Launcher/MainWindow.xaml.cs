using System.Reflection;
using System.Windows;
using CommunityToolkit.Mvvm.Messaging;
using Veritas.Services.Launcher.Message;
using Veritas.Services.Launcher.ViewModel;

namespace Veritas.Services.Launcher;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        WeakReferenceMessenger.Default.Send(new TitleChangedMessage($"Vision of the past (Version {Assembly.GetExecutingAssembly().GetName().Version})"));
    }
}