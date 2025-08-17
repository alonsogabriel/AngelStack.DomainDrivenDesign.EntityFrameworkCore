using ConsoleTests;
using DomainDrivenDesign.Abstractions.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using var context = new AppDbContext();

await context.Database.EnsureDeletedAsync();
await context.Database.MigrateAsync();
await context.AddCountriesAsync();
await context.AddRegionTypesAsync();
await context.AddRegionsAsync();

await context.SaveChangesAsync();

Console.WriteLine("Database created successfully!");