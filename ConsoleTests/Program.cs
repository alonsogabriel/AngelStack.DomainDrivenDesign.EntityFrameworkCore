using ConsoleTests;
using AngelStack.DomainDrivenDesign.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using var context = new AppDbContext();

await context.Database.EnsureDeletedAsync();
await context.Database.MigrateAsync();

await context.AddCountriesAsync();
await context.AddRegionTypesAsync();
await context.AddRegionsAsync();
await context.AddCitiesAsync();

Console.WriteLine("Database created successfully!");