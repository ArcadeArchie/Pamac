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
    Optional,
    Required
}

public enum TrustLevel
{
    TrustedOnly,
    TrustAll
}