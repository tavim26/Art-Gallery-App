using ArtistService.Infrastructure;
using ArtistService.Services;
using ArtistService.Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using ArtistService.Facade;

var builder = WebApplication.CreateBuilder(args);

// === Add services to the container ===
builder.Services.AddDbContext<ArtistDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// REGISTRARE DEPENDEN?E pentru serviciile artist
builder.Services.AddScoped<IArtistDAO, ArtistDAO>();
builder.Services.AddScoped<ArtistsService>();
builder.Services.AddScoped<ArtistsFacade>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// === Configure the HTTP request pipeline ===
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();