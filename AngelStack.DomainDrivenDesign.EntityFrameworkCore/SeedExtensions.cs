using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public static class SeedExtensions
{
    public static async Task AddCountriesAsync(this DbContext context)
    {
        await new CountrySeed(context).SeedAsync();
    }

    public static async Task AddRegionTypesAsync(this DbContext context)
    {
        await new RegionTypeSeed(context).SeedAsync();
    }

    public static async Task AddRegionsAsync(this DbContext context)
    {
        await new RegionSeed(context).SeedAsync();
    }

    public static async Task AddCitiesAsync(this DbContext context)
    {
        await new CitySeed(context).SeedAsync();
    }
}
