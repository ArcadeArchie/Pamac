using Avalonia;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Pamac.UI.Hosting.Lifetime;

using System;

using Avalonia.Controls.Templates;

namespace Pamac.UI.Hosting;

public static class HostBuilderExtensions
{
    public static IHostBuilder ConfigureAvaloniaAppBuilder<TApplication>(this IHostBuilder hostBuilder, Action<AppBuilder> configureAppBuilder, IHostedLifetime? lifetime = null)
        where TApplication : Application
    {
        ArgumentNullException.ThrowIfNull(configureAppBuilder);

        hostBuilder.ConfigureServices((ctx, s) =>
        {
            s.AddTransient<IDataTemplate, ViewLocator>();
            s.AddSingleton<TApplication>()
                .AddSingleton(provider =>
                {
                    AppBuilder appBuilder = AppBuilder.Configure(provider.GetRequiredService<TApplication>);
                    configureAppBuilder(appBuilder);
                    return appBuilder;
                });
            
            s.AddSingleton<Avalonia.Application>(svc => svc.GetRequiredService<AppBuilder>().Instance!);
            s.AddSingleton<IHostedLifetime>(p =>
                lifetime ?? HostedLifetime.Select(p.GetRequiredService<ILoggerFactory>(),
                    p.GetRequiredService<Avalonia.Application>().ApplicationLifetime));
        });

        return hostBuilder;
    }
}