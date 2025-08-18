using AngelStack.DomainDrivenDesign.Abstractions.Extensions;
using AngelStack.DomainDrivenDesign.Abstractions;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public static class ValueObjectMappings
{
    public static void MapStringValue<T, K>(this OwnedNavigationBuilder<T, K> builder,
    bool required = true, int maxLength = int.MaxValue, string? columnName = null)
    where T : class
    where K : StringValue
    {
        columnName ??= typeof(T).Name;

        builder.Property(s => s.Value)
            .IsRequired(required)
            .HasMaxLength(maxLength)
            .HasColumnName(columnName);
    }

    public static void MapStringValue<T, K>(
        this EntityTypeBuilder<T> builder, Expression<Func<T, K?>> property,
        bool required = true, int maxLength = int.MaxValue, string? columnName = null)
        where T : class
        where K : StringValue
    {
        if (property.Body is MemberExpression member)
        {
            columnName ??= member.Member.Name;
        }

        builder.OwnsOne(property).MapStringValue(required, maxLength, columnName);
    }

    public static void MapStringValidatable<T, K>(
        this OwnedNavigationBuilder<T, K> builder,
        string? columnName = null)
        where T : class
        where K : StringValidatable
    {
        bool required = StringValidatableExtensions.IsRequired<K>();
        int maxLength = StringValidatableExtensions.GetMaxLength<K>() ?? int.MaxValue;

        builder.MapStringValue(required, maxLength, columnName);
    }

    public static void MapStringValidatable<T, K>(
        this EntityTypeBuilder<T> builder, Expression<Func<T, K?>> property,
        string? columnName = null)
        where T : class
        where K : StringValidatable
    {
        if (property.Body is MemberExpression member)
        {
            columnName ??= member.Member.Name;
        }

        builder.OwnsOne(property).MapStringValidatable(columnName);
    }
}
