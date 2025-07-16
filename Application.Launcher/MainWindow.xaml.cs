using System.Reflection;
using System.Windows;
using System.Windows.Media.Animation;
using Application.Launcher.Message;
using Application.Launcher.ViewModel;
using CommunityToolkit.Mvvm.Messaging;

namespace Application.Launcher;

public partial class MainWindow : Window
{
    public MainWindow(MainWindowViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
        WeakReferenceMessenger.Default.Send(new TitleChangedMessage($"Vision of the past (Version {Assembly.GetExecutingAssembly().GetName().Version})"));
    }
    
    private void Window_Loaded(object sender, RoutedEventArgs e)
    {
        DoubleAnimation animation = new DoubleAnimation
        {
            From = 0.0,
            To = 1.0,
            Duration = new Duration(TimeSpan.FromMilliseconds(200))
        };

        Storyboard storyboard = new Storyboard();
        storyboard.Children.Add(animation);

        Storyboard.SetTarget(animation, this);
        Storyboard.SetTargetProperty(animation, new PropertyPath(OpacityProperty));

        storyboard.Begin();
    }
}