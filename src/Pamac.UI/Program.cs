using Avalonia;
using Avalonia.ReactiveUI;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using Pamac.UI.Hosting;
using Pamac.UI.Hosting.Lifetime;

using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

using Avalonia.Controls.Templates;

using Pamac.Core.Config;

namespace Pamac.UI;

internal static class Program
{
    [STAThread]
    public static async Task Main(string[] args)
    {
        var hostBuilder = Host.CreateDefaultBuilder(args);
        hostBuilder
            .AddConfig()
            .ConfigureServices(services =>
            {
            })
            .ConfigureAvaloniaAppBuilder<App>(appBuilder => appBuilder
                .UsePlatformDetect()
                .WithInterFont()
                .LogToTrace()
                .UseReactiveUI()
                .SetupWithClassicDesktopLifetime(args));

        var host = hostBuilder.Build();

        if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            // ReSharper disable once MethodHasAsyncOverload
            host.RunAvaloniaApp();
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ||
                 RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            await host.RunAvaloniaAppAsync();
        else
            throw new NotSupportedException($"Unsupported platform: {RuntimeInformation.OSDescription}");
    }

    /*
     // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);
     */
    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
        => AppBuilder.Configure<App>().UsePlatformDetect().WithInterFont().LogToTrace().UseReactiveUI();
}