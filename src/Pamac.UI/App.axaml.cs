using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Markup.Xaml;

namespace Pamac.UI;
public partial class App : Application
{
    private readonly IDataTemplate _viewLocator;

    public App(){ }
    public App(IDataTemplate viewLocator)
    {
        _viewLocator = viewLocator;
    }
    
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow();
        }
        DataTemplates.Add(_viewLocator);
        base.OnFrameworkInitializationCompleted();
    }
}