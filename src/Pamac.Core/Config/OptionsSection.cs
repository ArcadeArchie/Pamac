namespace Pamac.Core.Config;

/// <summary>
/// Option section of a pacman.conf
/// </summary>
/// <remarks>
/// see https://man.archlinux.org/man/pacman.conf.5 for details
/// </remarks>
public record OptionsSection
{
    #region Directories

    /// <summary>
    /// Set the default root directory for pacman to install to.
    /// </summary>
    public string RootDirectory { get; init; }

    /// <summary>
    /// Overrides the default location of the toplevel database directory.
    /// </summary>
    public string DbPath { get; init; }

    /// <summary>
    /// Overrides the default location of the package cache directory.
    /// </summary>
    public string CacheDirectory { get; init; }

    /// <summary>
    /// Add directories to search for alpm hooks in addition to the system hook directory
    /// </summary>
    public string HookDirectory { get; init; }

    /// <summary>
    /// Overrides the default location of the directory containing configuration files for GnuPG.
    /// </summary>
    public string GpgDirectory { get; init; }

    /// <summary>
    /// Overrides the default location of the pacman log file.
    /// </summary>
    public string LogFilePath { get; init; }

    /// <summary>
    /// Include another configuration file.
    /// </summary>
    public string IncludePath { get; init; }

    #endregion

    #region Flags

    /// <summary>
    /// Performs an approximate check for adequate available disk space before installing packages.
    /// </summary>
    public bool CheckSpace { get; init; }

    /// <summary>
    /// Disable defaults for low speed limit and timeout on downloads.
    /// </summary>
    public bool DisableDownloadTimeout { get; init; }

    /// <summary>
    /// Disable the default sandbox applied to the process downloading files on Linux systems.
    /// </summary>
    public bool DisableSandbox { get; init; }

    #endregion

    #region Enums

    /// <summary>
    /// If set to KeepInstalled (the default), the -Sc operation will clean packages that are no longer installed (not present in the local database). If set to KeepCurrent, -Sc will clean outdated packages (not present in any sync database).
    /// </summary>
    public string CleanMethod { get; init; }

    /// <summary>
    /// Set the default signature verification level.
    /// </summary>
    public string SignatureVerificationLevel { get; init; }

    /// <summary>
    /// Set the signature verification level for installing packages using the "-U" operation on a local file.
    /// </summary>
    public string LocalFileSignatureVerificationLevel { get; init; }

    /// <summary>
    /// If set, pacman will only allow installation of packages with the given architectures (e.g. i686, x86_64, etc).
    /// </summary>
    public string Architecture { get; init; }

    #endregion

    /// <summary>
    /// Specifies number of concurrent download streams.
    /// <remarks>The value needs to be a positive integer.</remarks>
    /// </summary>
    public int ParallelDownloads { get; init; }
    
    /// <summary>
    /// Specifies the user to switch to for downloading files.
    /// </summary>
    public string DownloadUsername { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string[] HeldPackages { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string[] IgnoredPackages { get; init; }

    /// <summary>
    /// 
    /// </summary>
    public string[] IgnoredGroups { get; init; }


    /// <summary>
    /// 
    /// </summary>
    public string ExternalDownloadProgram { get; init; }

    /// <summary>
    /// All files listed with a NoExtract directive will never be extracted from a package into the filesystem.
    /// </summary>
    public string[] NoExtract { get; init; }
}