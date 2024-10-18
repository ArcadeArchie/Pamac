namespace Pamac.Core.Config;
/// <summary>
/// 
/// </summary>
public record PacmanConfig(
    OptionsSection Options,
    RepositorySection[] Repositories);