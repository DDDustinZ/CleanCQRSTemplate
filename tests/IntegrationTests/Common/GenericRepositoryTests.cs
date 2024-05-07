using COMPANY_NAME.PRODUCT.Core.Abstracts;
using COMPANY_NAME.PRODUCT.Infrastructure.Data;
using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using UnitTests.Common;
using UnitTests.Common.SpecimenBuilders;

namespace IntegrationTests.Common;

public abstract class GenericRepositoryTests<TRepo, TEntity, TId>(IntegrationDbFixture fixture)
    : IntegrationDbTestBase(fixture)
    where TRepo : IGenericRepository<TEntity, TId>
    where TEntity : IEntity<TId>
{
    protected TRepo Sut { get; } = fixture.Services.GetRequiredService<TRepo>();
    protected AppDbContext DbContext { get; } = fixture.Services.GetRequiredService<AppDbContext>();

    [Theory]
    [AutoMoqData(typeof(IgnoreEntityIdBuilder))]
    public async Task GenericRepository_CRUD(TEntity newEntity, TEntity updateEntity)
    {
        var all = await Sut.GetAllAsync(CancellationToken.None);
        await Verify(all, VerifySettingsFactory.Default);

        Sut.Add(newEntity);
        await DbContext.SaveChangesAsync();

        DbContext.ChangeTracker.Clear();
        var hydratedEntity = await Sut.GetByIdAsync(newEntity.Id, CancellationToken.None);
        ReferenceEquals(newEntity, hydratedEntity).Should().BeFalse();
        newEntity.Should().BeEquivalentTo(hydratedEntity);

        DbContext.ChangeTracker.Clear();
        updateEntity.Id = hydratedEntity!.Id;
        Sut.Update(updateEntity);
        await DbContext.SaveChangesAsync();

        DbContext.ChangeTracker.Clear();
        var hydratedUpdatedEntity = await Sut.GetOneAsync(x => x.Id!.Equals(updateEntity.Id), CancellationToken.None);
        ReferenceEquals(updateEntity, hydratedUpdatedEntity).Should().BeFalse();
        updateEntity.Should().BeEquivalentTo(hydratedUpdatedEntity);

        DbContext.ChangeTracker.Clear();
        Sut.Delete(updateEntity);
        await DbContext.SaveChangesAsync();

        var refreshedAll = await Sut.GetAllAsync(CancellationToken.None);
        all.Should().BeEquivalentTo(refreshedAll);
    }
}