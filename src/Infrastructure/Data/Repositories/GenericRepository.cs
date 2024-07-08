using System.Linq.Expressions;
using COMPANY_NAME.PRODUCT.Core.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace COMPANY_NAME.PRODUCT.Infrastructure.Data.Repositories;

public abstract class GenericRepository<TEntity, TId>
    where TEntity : class, IEntity<TId>
{
    private readonly AppDbContext _context;

    protected GenericRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken ct)
    {
        return await _context.Set<TEntity>().ToListAsync(ct);
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct)
    {
        return await _context.Set<TEntity>().Where(filter).ToListAsync(ct);
    }

    public async Task<TEntity?> GetByIdAsync(TId id, CancellationToken ct)
    {
        return await _context.Set<TEntity>().FindAsync([id], ct);
    }

    public async Task<TEntity?> GetOneAsync(Expression<Func<TEntity, bool>> filter, CancellationToken ct)
    {
        return await _context.Set<TEntity>().Where(filter).SingleOrDefaultAsync(ct);
    }

    public void Add(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);
    }

    public void AddAll(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().AddRange(entities);
    }

    public void Delete(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);
    }

    public void DeleteAll(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().RemoveRange(entities);
    }

    public void Update(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);
    }
    
    public void UpdateAll(IEnumerable<TEntity> entities)
    {
        _context.Set<TEntity>().UpdateRange(entities);
    }
}