using SaleService.Infrastructure;
using SaleService.Domain.Contracts;
using SaleService.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<SaleDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// ?? Înregistrare servicii necesare
builder.Services.AddScoped<ISaleDAO, SaleDAO>();
builder.Services.AddScoped<SalesService>();
builder.Services.AddScoped<SalesFacade>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();