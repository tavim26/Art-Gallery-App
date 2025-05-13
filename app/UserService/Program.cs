using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure;
using UserService.Services;
using UserService.Services.Observers;
using UserService.Services.Notifications;
using UserService.Domain.Contracts;
using UserService.Services.Facade;

var builder = WebApplication.CreateBuilder(args);

// === Database Context ===
builder.Services.AddDbContext<UserDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// === DAO ===
builder.Services.AddScoped<IUserDAO, UserDAO>();

// === Core Services ===
builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UsersFacade>();

// === Notification strategies ===
builder.Services.AddSingleton<EmailNotificationStrategy>();
builder.Services.AddSingleton<SmsNotificationStrategy>();

// === Observers ===
builder.Services.AddSingleton<IUserObserver>(provider =>
    new EmailUserObserver(provider.GetRequiredService<EmailNotificationStrategy>()));
builder.Services.AddSingleton<IUserObserver>(provider =>
    new SmsUserObserver(provider.GetRequiredService<SmsNotificationStrategy>()));

// === Observer Subject (Notifier) ===
builder.Services.AddSingleton<UserUpdateNotifier>(provider =>
{
    var notifier = new UserUpdateNotifier();

    // Înregistrăm observatorii manual
    foreach (var observer in provider.GetServices<IUserObserver>())
    {
        notifier.RegisterObserver(observer);
    }

    return notifier;
});

// === MVC + Swagger ===
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// === Middleware ===
app.UseAuthorization();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.Run();