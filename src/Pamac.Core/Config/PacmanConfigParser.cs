namespace Pamac.Core.Config;

internal sealed class PacmanConfigParser
{
    private readonly Dictionary<string, string?> _data = [];

    private PacmanConfigParser() { }
    public static IDictionary<string, string?> Parse(Stream stream) => new PacmanConfigParser().ParseStream(stream);

    private Dictionary<string, string?> ParseStream(Stream stream)
    {
        using var streamReader = new StreamReader(stream);
        bool isOptionSection = false;
        string prefix = string.Empty;
        while (!streamReader.EndOfStream)
        {
            var line = streamReader.ReadLine();
            if (string.IsNullOrWhiteSpace(line)) continue;
            if (line.StartsWith('#')) continue;

            if (line.StartsWith('[')) // section start
            {
                isOptionSection = line[1..^1] == "options";
                if(!isOptionSection) prefix = line[1..^1]+"_";
                continue;
            }
            var parts = line.Split('=').Select(v => v.Trim()).ToArray();
            switch (parts.Length)
            {
                case 1 when isOptionSection:
                    _data.TryAdd(parts[0], null);
                    break;
                case 1 when !isOptionSection:
                    _data.TryAdd(prefix + parts[0], null);
                    break;
                case 2 when isOptionSection:
                    _data.TryAdd(parts[0], parts[1]);
                    break;
                case 2 when !isOptionSection:
                    _data.TryAdd(prefix + parts[0], parts[1]);
                    break;
            }
        }

        return _data;
    }
}