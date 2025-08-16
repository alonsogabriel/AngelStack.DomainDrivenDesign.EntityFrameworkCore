using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureCountry(this ModelBuilder builder)
    {
        return builder.ApplyConfiguration(new CountryMap());
    }

    public static ModelBuilder ConfigureRegion(this ModelBuilder builder)
    {
        return builder.ApplyConfiguration(new RegionMap());
    }

    public static ModelBuilder ConfigureRegionType(this ModelBuilder builder)
    {
        return builder.ApplyConfiguration(new RegionTypeMap());
    }
}