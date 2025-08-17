using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;

public class RegionTypeSeed(DbContext context) : AbstractSeedHandler<RegionType>(context)
{
    protected override Task<IEnumerable<RegionType>> GetDataAsync()
    {
        return Task.FromResult(DataLoader.LoadRegionTypes());
    }
}

