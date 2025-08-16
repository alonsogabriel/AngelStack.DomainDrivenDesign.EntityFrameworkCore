using AngelStack.DomainDrivenDesign.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public class BaseRepository<TContext, TEntity>(TContext context) : IRepository<TEntity>
    where TContext : DbContext
    where TEntity : class
{
    protected readonly TContext _context = context;

    protected DbSet<TEntity> Entities { get; } = context.Set<TEntity>();

    public async Task AddAsync(TEntity entity, CancellationToken cancellationToken)
    {
        await Entities.AddAsync(entity, cancellationToken);
    }

    public async Task<TEntity?> FindAsync(object id, CancellationToken cancellationToken)
    {
        return await Entities.FindAsync([id], cancellationToken);
    }

    public IQueryable<TEntity> Query()
    {
        return Entities.AsQueryable();
    }

    public void Remove(TEntity entity)
    {
        Entities.Remove(entity);
    }

    public void Update(TEntity entity)
    {
        Entities.Update(entity);
    }
}
