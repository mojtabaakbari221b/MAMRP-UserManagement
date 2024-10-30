using Microsoft.EntityFrameworkCore;
using UserManagement.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

// Add services to the container.

services.AddControllers();
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();
services.AddApplication();
services.AddInfrastructure(configuration);

services.AddDbContext<UserManagementDbContext>(options =>
    options.UseSqlServer(
        configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("04-Api")
    )
);

var app = builder.Build();

app.UseApplication();

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
