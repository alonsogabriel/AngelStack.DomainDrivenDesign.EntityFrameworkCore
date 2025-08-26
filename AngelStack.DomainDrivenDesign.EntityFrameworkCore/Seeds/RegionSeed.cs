using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;

public class RegionSeed(DbContext context) : AbstractSeedHandler<Region>(context)
{
    protected override async Task<IEnumerable<Region>> GetDataAsync()
    {
        var countries = await _context.Set<Country>().ToListAsync();
        var types = await _context.Set<RegionType>().ToListAsync();

        return DataLoader.LoadBrazilRegions(countries, types);
    }
}
