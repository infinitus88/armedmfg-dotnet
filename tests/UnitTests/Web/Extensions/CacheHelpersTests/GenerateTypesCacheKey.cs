using ArmedMFG.Web.Extensions;
using Xunit;

namespace ArmedMFG.UnitTests.Web.Extensions.CacheHelpersTests;

public class GenerateTypesCacheKey
{
    [Fact]
    public void ReturnsTypesCacheKey()
    {
        var result = CacheHelpers.GenerateTypesCacheKey();

        Assert.Equal("types", result);
    }
}
