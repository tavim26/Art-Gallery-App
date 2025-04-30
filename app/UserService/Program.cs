using UserService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UserService.Services;

var builder = WebApplication.CreateBuilder(args);

var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings.GetValue<string>("SecretKey");
builder.Services.AddSingleton(new JwtTokenGenerator(secretKey));



var smtpConfig = builder.Configuration.GetSection("Smtp");
var smtpEmail = smtpConfig.GetValue<string>("Email");
var smtpPassword = smtpConfig.GetValue<string>("Password");

builder.Services.AddSingleton<NotificationService>();


// DB Contexts
builder.Services.AddDbContext<UserDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
builder.Services.AddDbContext<AuthDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// MVC + Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseHttpsRedirection();


app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();