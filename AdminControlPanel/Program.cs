using AdminControlPanel.BL;
using AdminControlPanel.BL.Interfaces;
using AdminControlPanel.Repository;
using AdminControlPanel.Repository.Interfaces;
using AdminControlPanel.Repository.Mock;
using TaxiService.Repository.Customer.Interfaces;
using TaxiService.Repository.Customer.MockRepository;
using TaxiService.Repository.Driver.Interfaces;
using TaxiService.Repository.Driver.MockRepository;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IUserRepository, MockUsersRepository>();
builder.Services.AddScoped<IAccountLogicForAdmin, CustomerControlLogic>();

builder.Services.AddScoped<IDriverAccountRepository, MockDriverAccountRepository>();
builder.Services.AddScoped<IDriversLogicForAdmin, DriversControlLogic>();    

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