using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ConsoleTests.Entities;

internal class UserMap : BaseUserMap<User, int>
{
    public override void Configure(EntityTypeBuilder<User> builder)
    {
        base.Configure(builder);

        builder.HasOne(u => u.Person).WithOne().HasForeignKey<User>(u => u.PersonId).IsRequired();
    }
}
