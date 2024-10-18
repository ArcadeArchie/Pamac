using System.Diagnostics.CodeAnalysis;

namespace Pamac.Core.Config;
/// <summary>
/// Option section of a pacman.conf
/// </summary>
/// <remarks>
/// see https://man.archlinux.org/man/pacman.conf.5 for details
/// </remarks>
public record OptionsSection(
    string RootDirectory,
    string DbPath,
    string CacheDirectory,
    string HookDirectory,
    string GpgDirectory,
    string LogFilePath,
    string[] HeldPackages,
    string[] IgnoredPackages,
    string[] IgnoredGroups,
    string IncludePath,
    Architecture Architecture,
    string ExternalDownloadProgram,
    string[] NoExtract,
    CleanupMethod CleanMethod,
    SignatureVerificationLevel SignatureVerificationLevel,
    SignatureVerificationLevel LocalFileSignatureVerificationLevel,
    bool CheckSpace,
    bool DisableDownloadTimeout,
    int ParallelDownloads,
    string DownloadUsername,
    bool DisableSandbox);

public enum CleanupMethod
{
    KeepInstalled,
    KeepCurrent
}
public enum Architecture
{
    Auto,
    i686,
    x86_64
}