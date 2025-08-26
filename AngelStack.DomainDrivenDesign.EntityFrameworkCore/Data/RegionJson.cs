namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore.Data;

internal class RegionJson
{
    public int Id { get; init; }
    public string Country { get; init; }
    public string? Region { get; init; }
    public string Type { get; init; }
    public string Name { get; init; }
    public string? Alias { get; init; }
}