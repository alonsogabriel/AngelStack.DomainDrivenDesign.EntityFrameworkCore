using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class RegionTypeMap : IEntityTypeConfiguration<RegionType>
{
    public void Configure(EntityTypeBuilder<RegionType> builder)
    {
        builder.MapBaseEntity<RegionType, int>();

        builder.MapStringValidatable(r => r.Name);
    }
}