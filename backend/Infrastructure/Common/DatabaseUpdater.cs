using System.Reflection;
using DbUp;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Common;

public class DatabaseUpdater(IConfiguration configuration)
{
    public void Upgrade()
    {
        var connectionString = configuration.GetConnectionString("Default");

        var upgrade = DeployChanges.To
            .SqlDatabase(connectionString)
            .JournalToSqlTable("dbo", "SchemaVersions")
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .WithExecutionTimeout(TimeSpan.FromMinutes(5))
            .WithTransaction()
            .LogToConsole()
            .Build();

        var result = upgrade.PerformUpgrade();

        if (!result.Successful)
        {
            throw new Exception("Database upgrade failed");
        }

        Console.WriteLine("Database upgrade successful");
    }
}