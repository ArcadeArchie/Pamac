using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Pamac.Core.Config;

public static class ServiceCollectionExtensions
{
    public static IHostBuilder AddConfig(this IHostBuilder builder)
    {
        builder.ConfigureAppConfiguration((ctx, cfg) =>
        {
            cfg.Add<PacmanConfigurationSource>(s =>
            {
                s.Path = "pacman.test.conf";
                s.Optional = false;
                s.ReloadOnChange = true;
                s.FileProvider = null;
                s.ResolveFileProvider();
            });
        });
        builder.ConfigureServices((_, services) =>
        {
            services.AddOptions<PacmanConfig>("PacmanConf");
        });
        return builder;
    }
}