using Microsoft.Extensions.Configuration;

namespace Pamac.Core.Config;

public class PacmanConfigurationProvider : FileConfigurationProvider
{
    public PacmanConfigurationProvider(FileConfigurationSource source) : base(source)
    {
    }

    public override void Load(Stream stream)
    {
        try
        {
            Data = PacmanConfigParser.Parse(stream);
        }
        catch (Exception e)
        {
            throw;
        }
    }
}