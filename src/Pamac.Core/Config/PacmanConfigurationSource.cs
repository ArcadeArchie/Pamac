using Microsoft.Extensions.Configuration;

namespace Pamac.Core.Config;

public class PacmanConfigurationSource : FileConfigurationSource
{
    public override IConfigurationProvider Build(IConfigurationBuilder builder)
    {
        this.EnsureDefaults(builder);
        return new PacmanConfigurationProvider(this);
    }
}