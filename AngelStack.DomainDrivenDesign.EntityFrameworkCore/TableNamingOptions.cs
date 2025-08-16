namespace AngelStack.DomainDrivenDesign.EntityFrameworkCore;

public class TableNamingOptions
{
    public string? TableName { get; init; }
    public string? Schema { get; init; }
    public bool SnakeCaseColumns { get; init; } = false;
}