using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pamac.UI.Hosting.Lifetime;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;

namespace Pamac.UI.Hosting;
public static class HostExtensions
{
    [SupportedOSPlatform("macos")]
    public static int RunAvaloniaApp(this IHost host)
    {
        IHostedLifetime lifetime = host.Services.GetRequiredService<IHostedLifetime>();
        Avalonia.Application application = host.Services.GetRequiredService<Application>();
        int result = host.StartAsync(CancellationToken.None)
            .ContinueWith(_ => lifetime.StartAsync(application, CancellationToken.None).GetAwaiter().GetResult()).GetAwaiter().GetResult();

        Task.WaitAll(host.StopAsync(CancellationToken.None), host.WaitForShutdownAsync(CancellationToken.None));

        return result;
    }

    [SupportedOSPlatform("windows")]
    [SupportedOSPlatform("linux")]
    public async static Task<int> RunAvaloniaAppAsync(this IHost host, CancellationToken token = default)
    {
        IHostedLifetime lifetime = host.Services.GetRequiredService<IHostedLifetime>();
        Avalonia.Application application = host.Services.GetRequiredService<Application>();
        await host.StartAsync(token);
        int result = await lifetime.StartAsync(application, token);

        await host.StopAsync(token);

        await host.WaitForShutdownAsync(token);

        return result;
    }
}
