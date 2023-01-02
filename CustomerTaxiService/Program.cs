using CustomerTaxiService.BusinessLogic;
using CustomerTaxiService.BusinessLogic.Interfaces;
using CustomerTaxiService.Extensions;
using CustomerTaxiService.Extensions.Contracts;
using CustomerTaxiService.Extensions.Interfaces;
using CustomerTaxiService.Repository.Interfaces;
using CustomerTaxiService.Repository.MockRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); 

builder.Services.AddSingleton<ILoggerManager, LoggerManager>();
builder.Services.AddScoped<IOrdersLogic, OrdersLogic>();
builder.Services.AddScoped<IAccountLogic, AccountLogic>();
builder.Services.AddScoped<IRideRepository, MockRideRepository>();
builder.Services.AddScoped<IUserRepository, MockUsersRepository>();

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