using Avalonia.ReactiveUI;
using Pamac.UI.ViewModels;

namespace Pamac.UI;
public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
    }
}