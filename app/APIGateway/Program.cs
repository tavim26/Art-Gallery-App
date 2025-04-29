using Microsoft.Extensions.Configuration;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Adăugăm configurația pentru Ocelot
builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

// Adăugăm serviciile Ocelot
builder.Services.AddOcelot(builder.Configuration);
builder.Services.AddControllers();

var app = builder.Build();

// Middleware-ul standard
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Middleware-ul Ocelot (rutele către microservicii)
await app.UseOcelot();

app.Run();