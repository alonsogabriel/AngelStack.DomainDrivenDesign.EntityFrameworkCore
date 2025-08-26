using AngelStack.DomainDrivenDesign.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public class BaseUnitOfWork<TContext>(TContext context, IServiceProvider serviceProvider) : IUnitOfWork where TContext : DbContext
{
    protected readonly TContext _context = context;
    protected readonly IServiceProvider _serviceProvider = serviceProvider;
    public IRepository<T> GetRepository<T>() where T : class
    {
        return _serviceProvider.GetRequiredService<IRepository<T>>();
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }
}
