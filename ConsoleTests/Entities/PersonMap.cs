using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleTests.Entities;

internal class PersonMap : BasePersonMap<Person>
{
    public override void Configure(EntityTypeBuilder<Person> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.Address).WithOne().HasForeignKey<Person>(p => p.AddressId).IsRequired(false);
    }
}
