using System.Windows.Controls;
using Application.Launcher.ViewModel;

namespace Application.Launcher.View;

public partial class PatchView : UserControl
{
    public PatchView(PatchViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}