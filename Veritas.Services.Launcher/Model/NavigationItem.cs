namespace Veritas.Services.Launcher.Model;

public class NavigationItem
{
    public string Name { get; set; }
    public NavigationAction Action { get; set; }
    public string Icon { get; set; }
    
    public NavigationItem()
    {
        Name = string.Empty;
        Action = NavigationAction.Home;
        Icon = string.Empty;
    }
    
    public NavigationItem(string name, NavigationAction action, string icon)
    {
        Name = name;
        Action = action;
        Icon = icon;
    }
}