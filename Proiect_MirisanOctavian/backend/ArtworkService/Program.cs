using ArtworkService.Infrastructure;
using ArtworkService.Domain.Contracts;
using ArtworkService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDbContext<ArtworkDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IArtworkDAO, ArtworkDAO>();

builder.Services.AddDbContext<ArtworkImageDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddScoped<IArtworkImageDAO, ArtworkImageDAO>();

builder.Services.AddScoped<ArtworksService>();
builder.Services.AddScoped<ArtworkImagesService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();