using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pamac.UI.Hosting.Lifetime;
internal sealed class DesktopHostedLifetime : HostedLifetimeBase<IClassicDesktopStyleApplicationLifetime>
{
    private readonly ILogger<DesktopHostedLifetime> _logger;

    internal DesktopHostedLifetime(ILogger<DesktopHostedLifetime> logger, IClassicDesktopStyleApplicationLifetime runtime) : base(runtime)
    {
        _logger = logger;
    }
    public override async Task<int> StartAsync(Avalonia.Application application, CancellationToken cancellationToken)
    {
        if (this.Runtime is ClassicDesktopStyleApplicationLifetime lifetime)
        {
            int result;
            try
            {
                result = lifetime.Start(lifetime.Args ?? Array.Empty<string>());
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Critical))
                {
                    _logger.LogCritical(ex, "Failure while running application");
                }

                result = -1;
            }

            return await Task.FromResult(result);
        }

        return await new ControlledHostedLifetime(_logger, Runtime).StartAsync(application, cancellationToken);
    }

    public override async Task StopAsync(Avalonia.Application application, CancellationToken cancellationToken)
    {
        Window? mainWindow = Runtime.MainWindow;
        if (mainWindow is not null)
        {
            switch (Runtime.ShutdownMode)
            {
                case ShutdownMode.OnMainWindowClose:
                    await Task.Run(mainWindow.Close, cancellationToken);
                    return;
                case ShutdownMode.OnLastWindowClose:
                    foreach (Window window in Runtime.Windows)
                    {
                        if (!ReferenceEquals(mainWindow, window))
                        {
                            await Task.Run(window.Close, cancellationToken);
                        }
                    }

                    await Task.Run(mainWindow.Close, cancellationToken);
                    return;
                case ShutdownMode.OnExplicitShutdown:
                    await Task.Run(() => Runtime.Shutdown(), cancellationToken);
                    return;
            }
        }

        await new ControlledHostedLifetime(_logger, Runtime).StopAsync(application, cancellationToken);
    }
}
