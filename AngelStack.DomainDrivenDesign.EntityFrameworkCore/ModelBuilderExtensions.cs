using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static void ConfigureCountry(this ModelBuilder builder, TableNamingOptions? options = null)
    {
        builder.ApplyConfiguration(new CountryMap(options));
    }

    public static void ConfigureRegion(this ModelBuilder builder, TableNamingOptions? options = null)
    {
        builder.ApplyConfiguration(new RegionMap(options));
    }
}