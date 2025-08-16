using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AngelStack.DomainDrivenDesign.Abstractions;
using AngelStack.DomainDrivenDesign.Abstractions.Extensions;

namespace DomainDrivenDesign.Abstractions.EntityFrameworkCore;

public static class Extensions
{
    public static PropertyBuilder<K> Property<T, K>(
        this EntityTypeBuilder<T> builder,
        Expression<Func<T, K>> property,
        string? columnName)
        where T : class
    {
        return builder.Property<K>(property).HasColumnName(columnName);
    }

    public static void MapBaseEntity<T, K>(
        this EntityTypeBuilder<T> builder,
        string? tableName = null,
        string? schema = null,
        bool snakeCaseColumns = false) where T : AbstractEntity<K>
    {
        (string? idColumn, string? createdAtColumn, string? updatedAtColumn) =
            snakeCaseColumns ? ("id", "created_at", "updated_at") : (null, null, null);

        if (tableName != null)
        {
            builder.ToTable(tableName, schema);
        }

        builder.HasKey(e => e.Id);

        if (typeof(K) == typeof(Guid))
        {
            builder.Property(e => e.Id, idColumn).ValueGeneratedNever();
        }

        builder.Property(e => e.CreatedAt, createdAtColumn).IsRequired();
        builder.Property(e => e.UpdatedAt, updatedAtColumn).IsRequired(false);
    }

    public static void MapBaseEntity<T>(
        this EntityTypeBuilder<T> builder,
        string? tableName = null,
        string? schema = null) where T : AbstractEntity
    {
        builder.MapBaseEntity<T, Guid>(tableName, schema);
    }

    public static void MapSoftDelete<T>(
        this EntityTypeBuilder<T> builder,
        bool applyFilter = true) where T : class, ISoftDelete
    {
        builder.Property(e => e.DeletedAt).IsRequired(false);
        if (applyFilter)
        {
            builder.HasQueryFilter(e => e.DeletedAt == null);
        }
    }

    public static ModelBuilder MapEnum<T>(
        this ModelBuilder modelBuilder,
        string? tableName = null,
        string? schema = null,
        bool snakeCaseColumns = false) where T : AbstractEnum<T>
    {
        var builder = modelBuilder.Entity<T>();

        if (tableName != null)
        {
            builder.ToTable(tableName, schema);
        }

        (string? valueColumn, string? nameColumn) =
            snakeCaseColumns ? ("value", "name") : (null, null);

        builder.HasKey(e => e.Value);
        builder.Property(e => e.Value, valueColumn);
        builder.Property(e => e.Name, nameColumn).IsRequired().HasMaxLength(50);
        builder.HasIndex(e => e.Name).IsUnique();
        builder.HasData(AbstractEnum<T>.GetValues());

        return modelBuilder;
    }

    public static void MapEnumConversion<T>(this PropertyBuilder<T> builder)
        where T : struct, Enum
    {
        builder.HasConversion(v => v.ToString(), v => Enum.Parse<T>(v));
    }

    public static void MapEnumConversion<T>(this PropertyBuilder<T?> builder)
        where T : struct, Enum
    {
        builder.HasConversion(
            v => v != null ? v.ToString() : null,
            v => v != null ? Enum.Parse<T>(v) : null);
    }

    public static ReferenceCollectionBuilder<T, K> PreventDelete<T, K>(
        this ReferenceCollectionBuilder<T, K> builer) where T : class where K : class
    {
        return builer.OnDelete(DeleteBehavior.NoAction);
    }

    public static void MapStringValue<T, K>(this OwnedNavigationBuilder<T, K> builder,
        bool required = true, int maxLength = int.MaxValue, string? columnName = null)
        where T : class
        where K : StringValue
    {
        columnName ??= typeof(T).Name;

        builder.Property(s => s.Value).IsRequired(required).HasMaxLength(maxLength).HasColumnName(columnName);
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