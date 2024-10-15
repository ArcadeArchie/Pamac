using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pamac.UI.Hosting.Lifetime;
internal class ControlledHostedLifetime : HostedLifetimeBase<IControlledApplicationLifetime>
{
    private readonly ILogger<IHostedLifetime> _logger;

    internal ControlledHostedLifetime(ILogger<ControlledHostedLifetime> logger, IControlledApplicationLifetime runtime) : base(runtime)
    {
        _logger = logger;
    }

    internal ControlledHostedLifetime(ILogger<IHostedLifetime> logger, IControlledApplicationLifetime runtime) : base(runtime)
    {
        _logger = logger;
    }

    public override Task<int> StartAsync(Avalonia.Application application, CancellationToken cancellationToken)
    {
        int RunInControlledBackground()
        {
            try
            {
                application.Run(cancellationToken);
                return 0;
            }
            catch (Exception ex)
            {
                if (_logger.IsEnabled(LogLevel.Critical))
                {
                    _logger.LogCritical(ex, "Failure while running application");
                }

                return -1;
            }
        }

        return Task.Run(RunInControlledBackground, cancellationToken);
    }

    public override Task StopAsync(Avalonia.Application application, CancellationToken cancellationToken)
    {
        void ShutdownWithExitCodeZero()
        {
            Runtime.Shutdown(0);
        }

        return Task.Run(ShutdownWithExitCodeZero, cancellationToken);
    }
}
