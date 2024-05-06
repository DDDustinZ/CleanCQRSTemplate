using FluentAssertions;
using FluentAssertions.Equivalency;

namespace UnitTests.Common;

public static class CompareExtensions
{
    public static bool IsEquivalentTo(this object first, object second)
    {
        first.Should().BeEquivalentTo(second);
        return true;
    }
    
    public static bool IsEquivalentTo(this object first, object second, Func<EquivalencyAssertionOptions<object>, EquivalencyAssertionOptions<object>> options)
    {
        first.Should().BeEquivalentTo(second, options);
        return true;
    }
}