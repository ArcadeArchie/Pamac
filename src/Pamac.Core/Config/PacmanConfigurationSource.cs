using Microsoft.Extensions.Configuration;

namespace Pamac.Core.Config;

internal class PacmanConfigurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        this.EnsureDefaults(builder);
        return new PacmanConfigurationProvider(this);
    }
}