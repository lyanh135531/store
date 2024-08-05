using Domain.Business.Entities;
using Domain.Business.Repositories;
using Infrastructure.Core;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories.Business;

public class ProductRepository(ApplicationDbContext applicationDbContext)
    : EfCoreRepository<Product, Guid>(applicationDbContext), IProductRepository
{
    private readonly ApplicationDbContext _applicationDbContext = applicationDbContext;

    public async Task<int> GetNextSequenceNumberAsync()
    {
        const string sql = "SELECT NEXT VALUE FOR ProductCodeSequence";

        await using var command = _applicationDbContext.Database.GetDbConnection().CreateCommand();
        command.CommandText = sql;

        if (command.Connection != null && command.Connection.State != System.Data.ConnectionState.Open)
        {
            await command.Connection.OpenAsync();
        }

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt32(result);
    }
}