using KPMDotNetCore.RestApi.Db;
using KPMDotNetCore.Shared;
using Microsoft.EntityFrameworkCore;
using System.Data.SqlClient;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//add connection
string connectionString = builder.Configuration.GetConnectionString("DbConnection")!;
//add dependency injection
//builder.Services.AddScoped<AdoDotNetService>(n => new AdoDotNetService(connectionString));
//builder.Services.AddScoped<DapperService>(n => new DapperService(connectionString));

builder.Services.AddScoped(n => new AdoDotNetService(connectionString));
builder.Services.AddScoped(n => new DapperService(connectionString));

// For EFCore DbContext
builder.Services.AddDbContext<AppDbContext>(opt => 
{
    opt.UseSqlServer(connectionString);
},
ServiceLifetime.Transient,
ServiceLifetime.Transient);

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
