using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;

public class CountrySeed(DbContext context) : AbstractSeedHandler<Country>(context)
{
    protected override Task<IEnumerable<Country>> GetDataAsync()
    {
        return Task.FromResult(DataLoader.LoadCountries());
    }
}