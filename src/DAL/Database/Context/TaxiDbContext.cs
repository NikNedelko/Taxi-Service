using Microsoft.EntityFrameworkCore;

namespace DAL.Database.Context;

public class TaxiDbContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "TaxiDB");
    }
}