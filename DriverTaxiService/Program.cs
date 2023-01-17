using DriverTaxiService.BusinessLogic;
using DriverTaxiService.BusinessLogic.Interface;
using DriverTaxiService.Repository.Interfaces;
using DriverTaxiService.Repository.MockRepository;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IDriverAccountLogic, DriverAccountLogic>();
builder.Services.AddScoped<IDriveLogic, DriveLogic>();
builder.Services.AddScoped<IDriverAccountRepository, MockDriverAccountRepository>();
builder.Services.AddScoped<IDriveRepository, MockDriveRepository>();

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