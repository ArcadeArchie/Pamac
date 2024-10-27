using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;

namespace Pamac.UI;
public class App : Application
{
    private readonly IDataTemplate _viewLocator;
    /// <summary>
    /// Only needed to keep the Designer functioning
    /// </summary>
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    public App() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);
            desktop.MainWindow = new MainWindow();
        }
        if(!Design.IsDesignMode)
            DataTemplates.Add(_viewLocator);
        base.OnFrameworkInitializationCompleted();
    }
}