using Application.BL.Customer;
using Application.BL.Drive;
using Application.BL.DriverLogic;
using Application.BL.Order;
using DAL.Interfaces.Customer;
using DAL.Interfaces.Drive;
using DAL.Interfaces.Driver;
using DAL.Interfaces.Order;
using DAL.Mock.MockRepository;
using DAL.Repository.Interfaces.CustomerRepository;
using DAL.Repository.Interfaces.DriverRepository;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        //customers dependencies 
        services.AddScoped<IOrdersLogic, OrdersLogic>();
        services.AddScoped<IAccountLogic, AccountLogic>();
        services.AddScoped<IRideRepository, MockRideRepository>();
        services.AddScoped<IUserRepository, MockUsersRepository>();
        
        //drivers dependencies
        services.AddScoped<IDriveLogic, DriveLogic>();
        services.AddScoped<IDriveRepository, MockDriveRepository>();
        services.AddScoped<IDriverAccountLogic, DriverAccountLogic>();
        services.AddScoped<IDriverAccountRepository, MockDriverAccountRepository>();
        
        return services;
    }
}