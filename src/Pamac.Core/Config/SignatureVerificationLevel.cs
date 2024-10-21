namespace Pamac.Core.Config;

public struct SignatureVerificationLevel
{
    private ALPM_SIGLEVEL _level;
    private int mask;

    public SignatureVerificationLevel(int level)
    {
        _level = (ALPM_SIGLEVEL)level;
    }

    public static implicit operator int(SignatureVerificationLevel lvl) => (int)lvl._level;

    internal void SetLevel(ALPM_SIGLEVEL level)
    {
        _level |= level;
        mask |= (int)level;
    }
    internal void UnsetLevel(ALPM_SIGLEVEL level)
    {
        _level &= ~level;
        mask |= (int)level;
    }
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