using ConsoleTests;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using var context = new AppDbContext();

await context.Database.EnsureDeletedAsync();
await context.Database.MigrateAsync();

Console.WriteLine("Database created successfully!");