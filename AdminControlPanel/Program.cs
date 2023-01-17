using AdminControlPanel.BL;
using AdminControlPanel.BL.Interfaces;
using AdminControlPanel.Repository;
using AdminControlPanel.Repository.Interfaces;
using CustomerTaxiService.Repository.Interfaces;
using CustomerTaxiService.Repository.MockRepository;
using DriverTaxiService.Repository.Interfaces;
using DriverTaxiService.Repository.MockRepository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, MockUsersRepository>();
builder.Services.AddScoped<IAccountLogicForAdmin, CustomerControlLogic>();

builder.Services.AddScoped<IDriverAccountRepository, MockDriverAccountRepository>();
builder.Services.AddScoped<IDriversLogicForAdmin, DriversLogicForAdmin>();    

builder.Services.AddScoped<ICustomerAdminRepository, MockCustomerAdminRepository>();
builder.Services.AddScoped<IDriverAdminRepository, MockDriverAdminRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();