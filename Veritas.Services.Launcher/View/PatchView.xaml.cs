using System.Windows.Controls;
using Veritas.Services.Launcher.ViewModel;

namespace Veritas.Services.Launcher.View;

public partial class PatchView : UserControl
{
    public PatchView(PatchViewModel viewModel)
    {
        InitializeComponent();
        DataContext = viewModel;
    }
}