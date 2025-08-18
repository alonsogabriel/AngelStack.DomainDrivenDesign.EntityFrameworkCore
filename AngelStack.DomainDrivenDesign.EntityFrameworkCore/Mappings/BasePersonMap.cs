using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class BasePersonMap<T> : IEntityTypeConfiguration<T> where T : AbstractPerson
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.MapBaseEntity<T>();

        builder.MapStringValidatable(p => p.Name);

        builder.Property(p => p.Gender).MapEnumConversion();

        builder.OwnsOne(p => p.DateOfBirth, nav =>
        {
            nav.Property(d => d.Value).HasColumnName("DateOfBirth").IsRequired();
        });

        // TODO add PlaceOfBirthId to AbstractPerson on AngelStack.DomainDrivenDesign
        builder.HasOne(p => p.PlaceOfBirth).WithMany().HasForeignKey("PlaceOfBirthId").IsRequired(false);
    }
}
