namespace Pamac.Core.Config;
/// <summary>
/// 
/// </summary>
public record PacmanConfig()
{
    public OptionsSection Options { get; init; }
    public RepositorySection[] Repositories { get; init; }
}