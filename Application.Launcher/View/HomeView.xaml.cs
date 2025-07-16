using System.Windows.Controls;
using Application.Launcher.ViewModel;

namespace Application.Launcher.View;

public partial class HomeView : UserControl
{
    public HomeView(HomeViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}