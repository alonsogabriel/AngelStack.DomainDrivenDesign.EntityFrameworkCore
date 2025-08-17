using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class CityMap : IEntityTypeConfiguration<City>
{
    public void Configure(EntityTypeBuilder<City> builder)
    {
        builder.MapBaseEntity<City, int>();

        builder.MapStringValue(c => c.Name);

        builder.HasOne(c => c.Region).WithMany(r => r.Cities).HasForeignKey(c => c.RegionId).IsRequired();
    }
}