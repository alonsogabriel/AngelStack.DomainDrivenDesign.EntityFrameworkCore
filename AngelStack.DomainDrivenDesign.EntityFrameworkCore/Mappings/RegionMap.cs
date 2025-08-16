using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class RegionMap(TableNamingOptions? options = null) : IEntityTypeConfiguration<Region>
{
    public TableNamingOptions Options { get; } = options ?? new();
    public void Configure(EntityTypeBuilder<Region> builder)
    {
        builder.MapBaseEntity<Region, int>();

        builder.HasOne(r => r.Country).WithMany().HasForeignKey(r => r.CountryId).IsRequired().PreventDelete();

        builder.HasOne(r => r.Parent).WithMany(p => p.Regions).HasForeignKey(r => r.ParentId).IsRequired(false);

        builder.HasOne(r => r.Type).WithMany().HasForeignKey(r => r.TypeId).IsRequired();

        builder.MapStringValidatable(r => r.Name);

        builder.MapStringValidatable(r => r.Alias);
    }
}