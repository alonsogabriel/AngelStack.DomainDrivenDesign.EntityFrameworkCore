using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class AddressMap : IEntityTypeConfiguration<Address>
{
    public void Configure(EntityTypeBuilder<Address> builder)
    {
        builder.MapBaseEntity();

        builder.MapStringValidatable(a => a.ZipCode);
        builder.MapStringValidatable(a => a.Street);
        builder.MapStringValidatable(a => a.Neighborhood);
        builder.MapStringValidatable(a => a.Details);
        builder.MapStringValidatable(a => a.Number);

        builder.HasOne(a => a.City).WithMany().HasForeignKey(a => a.CityId).IsRequired();
    }
}
