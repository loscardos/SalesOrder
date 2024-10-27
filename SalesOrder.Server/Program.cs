using Microsoft.EntityFrameworkCore;
using SalesOrder.Server.BusinessProviders;
using SalesOrder.Server.BusinessProviders.Collection;
using SalesOrder.Server.DataProviders;
using SalesOrder.Server.DataProviders.Collection;
using SalesOrder.Server.Helper;
using SalesOrder.Server.Infrastructure;
using SalesOrder.Server.Validators;
using SalesOrder.Server.Validators.Collection;

var builder = WebApplication.CreateBuilder(args);

var configurationBuilder = new ConfigurationBuilder()
    .AddJsonFile(ApplicationConstant.AppSettingsPath, optional: true);

IConfiguration configuration = configurationBuilder.Build();

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.WriteIndented = true;
    });

builder.Services.AddDbContext<OlDevContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString(ApplicationConstant.DbContextConnectionStringSection)
    )
);

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<ISalesOrderBusinessProvider, SalesOrderBusinessProvider>();
builder.Services.AddTransient<ISalesOrderDataProvider, SalesOrderDataProvider>();
builder.Services.AddTransient<ISalesOrderValidator, SalesOrderValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();