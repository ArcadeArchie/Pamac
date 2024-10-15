using Avalonia.Controls.ApplicationLifetimes;
using System.Threading.Tasks;
using System.Threading;

namespace Pamac.UI.Hosting.Lifetime;

internal abstract class HostedLifetimeBase<TRuntime> : IHostedLifetime where TRuntime : IApplicationLifetime
{
    public int ExitCode { get; } = 0;

    protected HostedLifetimeBase(TRuntime runtime)
    {
        Runtime = runtime;
    }

    protected TRuntime Runtime { get; private init; }

    public abstract Task<int> StartAsync(Avalonia.Application application, CancellationToken cancellationToken);
    public abstract Task StopAsync(Avalonia.Application application, CancellationToken cancellationToken);
}
