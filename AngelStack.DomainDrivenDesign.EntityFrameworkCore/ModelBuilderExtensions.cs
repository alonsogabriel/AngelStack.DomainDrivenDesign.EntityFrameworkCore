using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public static class ModelBuilderExtensions
{
    public static ModelBuilder ConfigureCountry(this ModelBuilder builder)
        => builder.ApplyConfiguration(new CountryMap());

    public static ModelBuilder ConfigureGeographicEntities(this ModelBuilder builer)
    {
        return builer
            .ConfigureCountry()
            .ConfigureRegionType()
            .ConfigureRegion()
            .ConfigureCity();
    }

    private static ModelBuilder ConfigureRegion(this ModelBuilder builder) => builder.ApplyConfiguration(new RegionMap());
    private static ModelBuilder ConfigureRegionType(this ModelBuilder builder) => builder.ApplyConfiguration(new RegionTypeMap());
    private static ModelBuilder ConfigureCity(this ModelBuilder builder) => builder.ApplyConfiguration(new CityMap());
}