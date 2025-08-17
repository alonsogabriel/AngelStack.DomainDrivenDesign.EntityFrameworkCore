using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using AngelStack.DomainDrivenDesign.Abstractions;
using AngelStack.DomainDrivenDesign.Abstractions.Extensions;
using System.Text.RegularExpressions;
using AngelStack.DomainDrivenDesign.Entities;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore.Seeds;

namespace DomainDrivenDesign.Abstractions.EntityFrameworkCore;

public static partial class Extensions
{
    public static void MapBaseEntity<T, K>(
        this EntityTypeBuilder<T> builder,
        string? tableName = null,
        string? schema = null,
        bool idGenerated = true) where T : AbstractEntity<K>
    {
        if (tableName != null)
        {
            builder.ToTable(tableName, schema);
        }

        builder.HasKey(e => e.Id);

        if (!idGenerated)
        {
            builder.Property(e => e.Id).ValueGeneratedNever();
        }

        builder.Property(e => e.CreatedAt).IsRequired();
        builder.Property(e => e.UpdatedAt).IsRequired(false);
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
        string? schema = null) where T : AbstractEnum<T>
    {
        var builder = modelBuilder.Entity<T>();

        if (tableName != null)
        {
            builder.ToTable(tableName, schema);
        }

        builder.HasKey(e => e.Value);
        builder.Property(e => e.Value);
        builder.Property(e => e.Name).IsRequired().HasMaxLength(50);
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

    public static async Task AddCountriesAsync(this DbContext context)
    {
        await new CountrySeed(context).SeedAsync();
    }

    public static async Task AddRegionTypesAsync(this DbContext context)
    {
        await new RegionTypeSeed(context).SeedAsync();
    }

    public static async Task AddRegionsAsync(this DbContext context)
    {
        await new RegionSeed(context).SeedAsync();
    }

    public static async Task AddCitiesAsync(this DbContext context)
    {
        await new CitySeed(context).SeedAsync();
    }

    public static string ToSnakeCase(this string value)
    {
        var chrs = value.Select((c, i) =>
        {
            int p = i - 1;
            int n = i + 1;

            bool split =
                i > 0
                && char.IsUpper(c)
                && value[p] != '_'
                && (
                    !char.IsUpper(value[p])
                    || n < value.Length && !char.IsUpper(value[n])
                );

            return split ? "_" + c : c.ToString();
        });

        return MultipleUnderscoresRegex()
            .Replace(string.Concat(chrs).ToLower(), "_");
    }

    [GeneratedRegex("_{2,}", RegexOptions.Compiled)]
    private static partial Regex MultipleUnderscoresRegex();
}