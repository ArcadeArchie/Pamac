using Avalonia.Controls;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pamac.UI.Hosting.Lifetime;
internal sealed class FallbackHostedLifetime : IHostedLifetime
{
    private readonly ILogger<FallbackHostedLifetime> _logger;

    internal FallbackHostedLifetime(ILogger<FallbackHostedLifetime> logger)
    {
        _logger = logger;
    }

    public Task<int> StartAsync(Avalonia.Application application, CancellationToken cancellationToken)
    {
        int RunWithCancellationToken()
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
        return Task.Run(RunWithCancellationToken, cancellationToken);
    }

    public Task StopAsync(Avalonia.Application application, CancellationToken cancellationToken)
    {
        return Task.FromException<NotSupportedException>(new NotSupportedException());
    }
}
