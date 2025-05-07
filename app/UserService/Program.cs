using UserService.Infrastructure;
using Microsoft.EntityFrameworkCore;
using UserService.Services.Notifications;
using UserService.Services;
using UserService.Domain.Contracts;
var builder = WebApplication.CreateBuilder(args);

// DB Context
builder.Services.AddDbContext<UserDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// Adăugăm legătura dintre interfață și implementare
builder.Services.AddScoped<IUserDAO, UserDAO>();

// UsersService
builder.Services.AddScoped<UsersService>();

// Notification strategies
builder.Services.AddSingleton<INotificationStrategy, EmailNotificationStrategy>();
builder.Services.AddSingleton<INotificationStrategy, SmsNotificationStrategy>();
builder.Services.AddSingleton<NotificationService>(provider =>
{
    var config = provider.GetRequiredService<IConfiguration>();
    var emailStrategy = new EmailNotificationStrategy(config);
    var smsStrategy = new SmsNotificationStrategy(config);
    return new NotificationService(emailStrategy, smsStrategy);
});

// MVC + Swagger
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
