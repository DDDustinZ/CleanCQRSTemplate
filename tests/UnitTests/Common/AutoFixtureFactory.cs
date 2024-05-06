using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Kernel;

namespace UnitTests.Common;

public class AutoFixtureFactory
{
    public static Fixture GetDefaultFixture()
    {
        var autoFixture = new Fixture();
        autoFixture.Customize(new AutoMoqCustomization { ConfigureMembers = true });

        // Add any default builders here
        
        return autoFixture;
    }

    public static Fixture GetCustomizedFixture(params Type[] customizations)
    {
        var fixture = GetDefaultFixture();

        foreach (var type in customizations)
        {
            var customization = Activator.CreateInstance(type) as ISpecimenBuilder;
            fixture.Customizations.Insert(0, customization);
        }

        return fixture;
    }
}