using AngelStack.DomainDrivenDesign.Abstractions.Extensions;
using AngelStack.DomainDrivenDesign.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using AngelStack.DomainDrivenDesign.ValueObjects;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public static class ValueObjectMappings
{
    public static void MapStringValue<T, K>(this OwnedNavigationBuilder<T, K> builder,
    bool required = true, int? maxLength = null, string? columnName = null,
    Action<PropertyBuilder<string>>? configure = null)
    where T : class
    where K : StringValue
    {
        var prop = builder.Property(s => s.Value)
            .IsRequired(required)
            .HasColumnName(columnName);

        if (maxLength is not null)
        {
            prop.HasMaxLength(maxLength.Value);
        }

        configure?.Invoke(prop);
    }

    public static void MapStringValue<T, K>(
        this EntityTypeBuilder<T> builder, Expression<Func<T, K?>> property,
        bool required = true, int? maxLength = null, string? columnName = null,
        Action<PropertyBuilder<string>>? configure = null)
        where T : class
        where K : StringValue
    {
        if (property.Body is MemberExpression member)
        {
            columnName ??= member.Member.Name;
        }

        builder.OwnsOne(property).MapStringValue(required, maxLength, columnName, configure);
    }

    public static void MapStringValidatable<T, K>(this OwnedNavigationBuilder<T, K> builder,
        string? columnName = null, Action<PropertyBuilder<string>>? configure = null)
        where T : class
        where K : StringValidatable
    {
        bool required = StringValidatableExtensions.IsRequired<K>();
        int maxLength = StringValidatableExtensions.GetMaxLength<K>() ?? int.MaxValue;

        builder.MapStringValue(required, maxLength, columnName, configure);
    }

    public static void MapStringValidatable<T, K>(
        this EntityTypeBuilder<T> builder, Expression<Func<T, K?>> property,
        string? columnName = null, Action<PropertyBuilder<string>>? configure = null)
        where T : class
        where K : StringValidatable
    {
        if (property.Body is MemberExpression member)
        {
            columnName ??= member.Member.Name;
        }

        builder.OwnsOne(property).MapStringValidatable(columnName, configure);
    }

    public static void MapPhoneNumber<T>(this EntityTypeBuilder<T> builder,
        Expression<Func<T, PhoneNumber?>> property) where T : class
    {
        builder.MapStringValidatable(property);

        builder.OwnsOne(property, phone =>
        {
            phone.OwnsOne(n => n.CountryCode, code =>
            {
                code.Property(c => c.Value).IsRequired().HasColumnName("PhoneCountryCode");
            });
        });
    }
}
