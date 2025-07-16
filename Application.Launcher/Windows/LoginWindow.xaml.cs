using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using Application.Launcher.ViewModel.Windows;

namespace Application.Launcher.Windows
{
    public partial class LoginWindow : Window
    {
        private readonly LoginWindowViewModel _viewModel;
        
        public LoginWindow(LoginWindowViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            DataContext = viewModel;
        }

        private void UIElement_OnMouseDown(object sender, MouseButtonEventArgs e)
        {
            var instance = GetWindow(this);
            instance?.DragMove();
        }

        private void UIElement_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                _viewModel.RequestLogin();
            }
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
}
