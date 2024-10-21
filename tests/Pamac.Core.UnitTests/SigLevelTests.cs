using Pamac.Core.Config;

namespace Pamac.Core.UnitTests;

public class SigLevelTests
{
    [Fact]
    public void Test1()
    {
        var testLvl = new SignatureVerificationLevel();
        testLvl.SetLevel(ALPM_SIGLEVEL.ALPM_SIG_PACKAGE_MARGINAL_OK);
        testLvl.SetLevel(ALPM_SIGLEVEL.ALPM_SIG_DATABASE_MARGINAL_OK);
        var compare = ALPM_SIGLEVEL.ALPM_SIG_PACKAGE_MARGINAL_OK | ALPM_SIGLEVEL.ALPM_SIG_DATABASE_MARGINAL_OK;
        Assert.True(((int)testLvl) == (int)compare);
    }
}