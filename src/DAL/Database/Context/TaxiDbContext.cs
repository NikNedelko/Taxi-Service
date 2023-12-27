using Domain.Entities.CustomerData;
using Domain.Entities.DriverData;
using Domain.Entities.RideData;
using Microsoft.EntityFrameworkCore;

namespace DAL.Database.Context;

public class DatabaseContext : DbContext
{
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase(databaseName: "Taxi_Service_Database");
    }

    public DbSet<CustomerDB> Customers;
    public DbSet<DriverDb> Drivers;
    public DbSet<RideDb> Rides;
}