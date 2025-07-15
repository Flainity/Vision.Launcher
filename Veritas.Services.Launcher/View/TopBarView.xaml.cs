using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Veritas.Services.Launcher.View;

public partial class TopBarView : UserControl
{
    public TopBarView()
    {
        InitializeComponent();
    }

    private void TopBarView_OnMouseDown(object sender, MouseButtonEventArgs e)
    {
        var instance = Window.GetWindow(this);
        instance?.DragMove();
    }
}