using AutoFixture.Xunit2;

namespace UnitTests.Common;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(AutoFixtureFactory.GetDefaultFixture)
    { }

    public AutoMoqDataAttribute(params Type[] customizations)
        : base(() => AutoFixtureFactory.GetCustomizedFixture(customizations))
    { }
}

public class InlineAutoMoqDataAttribute : InlineAutoDataAttribute
{
    public InlineAutoMoqDataAttribute(params object[] objects) : base(new AutoMoqDataAttribute(), objects) { }

    public InlineAutoMoqDataAttribute(Type[] customizations, params object[] objects) : base(new AutoMoqDataAttribute(customizations), objects) { }
}