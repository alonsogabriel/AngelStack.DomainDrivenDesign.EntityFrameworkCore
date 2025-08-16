using AngelStack.DomainDrivenDesign.Entities;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Mappings;

public class CountryMap(TableNamingOptions? options = null) : IEntityTypeConfiguration<Country>
{
    public TableNamingOptions Options { get; } = options ?? new();

    public void Configure(EntityTypeBuilder<Country> builder)
    {
        builder.MapBaseEntity<Country, int>(Options.TableName, Options.Schema, Options.SnakeCaseColumns);

        var nameColumn = Options.SnakeCaseColumns ? "name" : null;

        builder.MapStringValidatable(c => c.Name, nameColumn);
    }
}
