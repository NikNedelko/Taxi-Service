using TaxiService.BusinessLogic.Customer;
using TaxiService.BusinessLogic.Customer.Interfaces;
using TaxiService.BusinessLogic.Driver;
using TaxiService.BusinessLogic.Driver.Interface;
using TaxiService.Extensions.ExceptionHandler;
using TaxiService.Extensions.ExceptionHandler.Contracts;
using TaxiService.Extensions.ExceptionHandler.Interfaces;
using TaxiService.Repository.Customer.Interfaces;
using TaxiService.Repository.Customer.MockRepository;
using TaxiService.Repository.Driver.Interfaces;
using TaxiService.Repository.Driver.MockRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
//customers dependencies 
builder.Services.AddScoped<IOrdersLogic, OrdersLogic>();
builder.Services.AddScoped<IAccountLogic, AccountLogic>();
builder.Services.AddScoped<IRideRepository, MockRideRepository>();
builder.Services.AddScoped<IUserRepository, MockUsersRepository>();
//drivers dependencies
builder.Services.AddScoped<IDriveLogic, DriveLogic>();
builder.Services.AddScoped<IDriveRepository, MockDriveRepository>();
builder.Services.AddScoped<IDriverAccountLogic, DriverAccountLogic>();
builder.Services.AddScoped<IDriverAccountRepository, MockDriverAccountRepository>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

var logger = app.Services.GetRequiredService<ILoggerManager>();
app.ConfigureExceptionHandler(logger);

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();