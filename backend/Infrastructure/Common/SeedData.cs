using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common;

public static class SeedData
{
    public static void Seed(ApplicationDbContext context, string seedFolderPath)
    {
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }

        var seedFiles = Directory.GetFiles(seedFolderPath, "*.sql");
        foreach (var file in seedFiles)
        {
            var sql = File.ReadAllText(file);
            context.Database.ExecuteSqlRaw(sql);
        }
    }
}