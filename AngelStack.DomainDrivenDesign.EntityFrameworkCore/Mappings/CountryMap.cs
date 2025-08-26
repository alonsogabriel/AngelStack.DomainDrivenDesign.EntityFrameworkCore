using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class CountryMap : IEntityTypeConfiguration<Country>
{
    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.MapBaseEntity<Country, int>();

        builder.MapStringValidatable(c => c.Name);

        builder.MapStringValidatable(c => c.IsoA2);

        builder.MapStringValidatable(c => c.IsoA3);

        builder.MapStringValidatable(c => c.IsoNumber);
    }
}
