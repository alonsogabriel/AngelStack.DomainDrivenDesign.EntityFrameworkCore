using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class CustomAddressMap : IEntityTypeConfiguration<CustomAddress>
{
    public void Configure(EntityTypeBuilder<CustomAddress> builder)
    {
        builder.MapBaseEntity();

        builder.MapStringValidatable(a => a.ZipCode);
        builder.MapStringValidatable(a => a.Part1);
        builder.MapStringValidatable(a => a.Part2);
        builder.MapStringValidatable(a => a.Part3);
        builder.MapStringValidatable(a => a.Details);
        builder.MapStringValidatable(a => a.Number);

        builder.HasOne(a => a.City).WithMany().HasForeignKey(a => a.CityId).IsRequired();
    }
}
