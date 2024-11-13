var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
var services = builder.Services;
{
    services.AddApplication()
        .AddInfrastructure(configuration)
        .AddApiServices(configuration)
        .AddShared();
}
var app = builder.Build();
{
    app.UseSeedingData();
    
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseExceptionHandling();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();
}