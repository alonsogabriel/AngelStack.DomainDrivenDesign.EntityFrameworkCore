using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class BaseUserMap<TUser, TId> : IEntityTypeConfiguration<TUser> where TUser : AbstractUser<TId>
{
    public virtual void Configure(EntityTypeBuilder<TUser> builder)
    {
        builder.MapBaseEntity<TUser, TId>();

        builder.MapStringValidatable(u => u.Username);

        builder.MapStringValidatable(u => u.Email);

        builder.MapPhoneNumber(u => u.PhoneNumber);
    }
}
