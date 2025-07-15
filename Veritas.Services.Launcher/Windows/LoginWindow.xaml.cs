using System.Windows;
using Veritas.Services.Launcher.ViewModel.Windows;

namespace Veritas.Services.Launcher.Windows
{
    public partial class LoginWindow : Window
    {
        public LoginWindow(LoginWindowViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel;
        }
    }
}
