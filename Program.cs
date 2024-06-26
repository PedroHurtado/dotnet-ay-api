using Controller;
using IOC;
using Segregation;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//https://learn.microsoft.com/es-es/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0

builder.Services.AddScoped<IB, B>();
builder.Services.AddScoped<IService, Service>();
builder.Services.AddScoped<IC, C>();

builder.Services.AddScoped(typeof(IAll<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IUpdate<,>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IRemove<,>), typeof(Repository<,>));
//builder.Services.AddScoped(typeof(IAdd<>), typeof(Repository<,>));
builder.Services.AddScoped(typeof(IGet<,>), typeof(Repository<,>));
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();


//builder.Services.AddTransient<IService, Service>();
//builder.Services.AddScoped<IService, Service>();
//builder.Services.AddSingleton<IService, Service>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseExceptionHandler();

app.UseHttpsRedirection();

app.MapControllers();

/*app.UseEndpoints(c=>{

});*/

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};


app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();



app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
