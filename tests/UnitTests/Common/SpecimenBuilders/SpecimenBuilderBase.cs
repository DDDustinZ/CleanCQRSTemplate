using AutoFixture.Kernel;

namespace UnitTests.Common.SpecimenBuilders;

public abstract class SpecimenBuilderBase<T> : ISpecimenBuilder
{
    public object? Create(object request, ISpecimenContext context)
    {
        if (request as Type != typeof(T))
        {
            return new NoSpecimen();
        }

        return CreateObject(request, context);
    }

    protected abstract T? CreateObject(object request, ISpecimenContext context);
}