namespace Pamac.Core.Config;

public struct SignatureVerificationLevel
{
    public VerificationLevel VerificationLevel { get; init; } = VerificationLevel.Optional;
    public TrustLevel TrustLevel { get; init; } = TrustLevel.TrustedOnly;

    public SignatureVerificationLevel(VerificationLevel verificationLevel, TrustLevel trustLevel)
    {
        VerificationLevel = verificationLevel;
        TrustLevel = trustLevel;
    }
}

public enum VerificationLevel
{
    Never,
    Required,
    Optional
}

public enum TrustLevel
{
    TrustedOnly,
    TrustAll
}
//https://gitlab.archlinux.org/pacman/pacman/-/blob/master/src/pacman/conf.c?ref_type=heads#L491
internal enum ALPM_SIGLEVEL
{
    ALPM_SIG_PACKAGE = (1 << 0),
    ALPM_SIG_PACKAGE_OPTIONAL = (1 << 1),
    ALPM_SIG_PACKAGE_MARGINAL_OK = (1 << 2),
    ALPM_SIG_PACKAGE_UNKNOWN_OK = (1 << 3),
    ALPM_SIG_DATABASE = (1 << 10),
    ALPM_SIG_DATABASE_OPTIONAL = (1 << 11),
    ALPM_SIG_DATABASE_MARGINAL_OK = (1 << 12),
    ALPM_SIG_DATABASE_UNKNOWN_OK = (1 << 13),
    ALPM_SIG_USE_DEFAULT = (1 << 30),
}