using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;

public abstract class AbstractSeedHandler<T>(DbContext context) where T : class
{
    protected readonly DbContext _context = context;

    public async Task SeedAsync()
    {
        var entities = _context.Set<T>();

        if (await entities.AnyAsync()) return;

        var data = await GetDataAsync();
        await entities.AddRangeAsync(data);
        await _context.SaveChangesAsync();
    }

    protected abstract Task<IEnumerable<T>> GetDataAsync();
}
