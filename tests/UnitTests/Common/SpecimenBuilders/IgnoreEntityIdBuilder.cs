using System.Reflection;
using AutoFixture.Kernel;

namespace UnitTests.Common.SpecimenBuilders;

public class IgnoreEntityIdBuilder : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        var propInfo = request as PropertyInfo;
        if (propInfo != null && 
            propInfo.Name == "Id" &&
            propInfo.DeclaringType != null && 
            propInfo.DeclaringType.GetInterfaces().Any(x => x.Name.StartsWith("IEntity")))
        {
            return new OmitSpecimen();
        }

        return new NoSpecimen();
    }
}