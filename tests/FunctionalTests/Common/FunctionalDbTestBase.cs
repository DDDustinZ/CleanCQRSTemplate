using FastEndpoints.Testing;

namespace FunctionalTests.Common;

[CollectionDefinition(nameof(FunctionalDbCollection))]
public class FunctionalDbCollection : TestCollection<FunctionalDbFixture>
{
}

[Collection(nameof(FunctionalDbCollection))]
public abstract class FunctionalDbTestBase(FunctionalDbFixture fixture) : TestBase
{
    protected override async Task SetupAsync()
    {
        await fixture.ResetDb();
    }
}