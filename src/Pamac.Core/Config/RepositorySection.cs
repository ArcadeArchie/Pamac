using System.Diagnostics.CodeAnalysis;

namespace Pamac.Core.Config;

public record RepositorySection(
    string Name,
    [StringSyntax(
        StringSyntaxAttribute.Uri)]string DownloadServer,
    string IncludePath,
    [StringSyntax(
        StringSyntaxAttribute.Uri)]string CacheServerUrl,
    string SignatureVerificationLevel,
    RepositoryUsage Usage);

public enum RepositoryUsage
{
    Sync,
    Search,
    Install,
    Upgrade,
    All
}