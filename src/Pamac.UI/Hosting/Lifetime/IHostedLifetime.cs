using Avalonia.Controls.ApplicationLifetimes;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pamac.UI.Hosting.Lifetime;
public interface IHostedLifetime
{
    Task<int> StartAsync(Avalonia.Application application, CancellationToken cancellationToken);

    Task StopAsync(Avalonia.Application application, CancellationToken cancellationToken);
}
internal static class HostedLifetime
{
    internal static IHostedLifetime Select(ILoggerFactory loggerFactory, IApplicationLifetime? lifetime)
    {
        return lifetime switch
        {
            IClassicDesktopStyleApplicationLifetime desktop => new DesktopHostedLifetime(loggerFactory.CreateLogger<DesktopHostedLifetime>(), desktop),
            IControlledApplicationLifetime controlled => new ControlledHostedLifetime(loggerFactory.CreateLogger<ControlledHostedLifetime>(), controlled),
            ISingleViewApplicationLifetime _ => throw new PlatformNotSupportedException("This is only supposed to run on windows, linux and macos. Mobile lifetimes are not supported."),
            _ => new FallbackHostedLifetime(loggerFactory.CreateLogger<FallbackHostedLifetime>()),
        };
    }
}