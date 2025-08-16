using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class RegionMap : IEntityTypeConfiguration<Region>
{
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.MapBaseEntity<Region, int>();

        builder.HasOne(r => r.Country).WithMany().HasForeignKey(r => r.CountryId).IsRequired().PreventDelete();

        builder.HasOne(r => r.Parent).WithMany(p => p.Regions).HasForeignKey(r => r.ParentId).IsRequired(false);

        builder.HasOne(r => r.Type).WithMany().HasForeignKey(r => r.TypeId).IsRequired();

        builder.MapStringValidatable(r => r.Name);

        builder.MapStringValidatable(r => r.Alias);

        // TODO remove and map cities
        builder.Ignore(r => r.Cities);
        builder.Ignore(r => r.AllCities);
    }
}