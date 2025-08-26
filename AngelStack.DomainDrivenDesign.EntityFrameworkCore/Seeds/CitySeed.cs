using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;

public class CitySeed(DbContext context) : AbstractSeedHandler<City>(context)
{
    protected override async Task<IEnumerable<City>> GetDataAsync()
    {
        var regions = await _context.Set<Region>()
            .Where(r => r.Type.Name.Value == "State"
            && r.Country.Name.Value == "Brazil").ToListAsync();

        return DataLoader.LoadBrazilCities(regions);
    }
}