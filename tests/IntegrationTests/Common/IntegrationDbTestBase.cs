using FastEndpoints.Testing;

namespace IntegrationTests.Common;

[CollectionDefinition(nameof(IntegrationDbCollection))]
public class IntegrationDbCollection : TestCollection<IntegrationDbFixture>
{
}

[Collection(nameof(IntegrationDbCollection))]
public abstract class IntegrationDbTestBase(IntegrationDbFixture fixture) : TestBase
{
    protected override async Task SetupAsync()
    {
        await fixture.ResetDb();
    }
}