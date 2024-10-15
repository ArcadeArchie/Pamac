using Avalonia;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Pamac.UI.Hosting.Lifetime;
using System;

namespace Pamac.UI.Hosting;
public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureAvalonia<TApplication>(this IHostBuilder hostBuilder, Action<AppBuilder> configureAppBuilder, IHostedLifetime? lifetime = null) where TApplication : Avalonia.Application, new()
    {
        return hostBuilder.ConfigureAvalonia(() => new TApplication(), configureAppBuilder, lifetime);
    }

    public static IHostBuilder ConfigureAvalonia<TApplication>(this IHostBuilder hostBuilder, Func<TApplication> applicationResolver, Action<AppBuilder> configureAppBuilder, IHostedLifetime? lifetime = null) where TApplication : Avalonia.Application
    {
        return hostBuilder.ConfigureAvaloniaAppBuilder<TApplication>(() => AppBuilder.Configure(applicationResolver), configureAppBuilder, lifetime);
    }

    public static IHostBuilder ConfigureAvaloniaAppBuilder<TApplication>(this IHostBuilder hostBuilder, Func<AppBuilder> appBuilderResolver, Action<AppBuilder> configureAppBuilder, IHostedLifetime? lifetime = null) where TApplication : Avalonia.Application
    {
        ArgumentNullException.ThrowIfNull(configureAppBuilder);

        hostBuilder.ConfigureServices((ctx, s) => {
            AppBuilder appBuilder = appBuilderResolver();
            configureAppBuilder(appBuilder);
            s.AddSingleton(appBuilder);
            if (appBuilder.Instance is null)
            {
                appBuilder.SetupWithoutStarting();
            }
            s.AddSingleton<Avalonia.Application>((_) => appBuilder.Instance!);
            s.AddSingleton<TApplication>((_) => (TApplication)appBuilder.Instance!);
            s.AddSingleton<IHostedLifetime>(p => lifetime ?? HostedLifetime.Select(p.GetRequiredService<ILoggerFactory>(), p.GetRequiredService<Avalonia.Application>().ApplicationLifetime));
        });

        return hostBuilder;
    }
}
