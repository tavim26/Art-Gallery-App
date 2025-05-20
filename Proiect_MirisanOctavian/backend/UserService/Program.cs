using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure;
using UserService.Services;
using UserService.Services.Observers;
using UserService.Services.Notifications;
using UserService.Domain.Contracts;
using UserService.Services.Facade;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<UserDAO>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

builder.Services.AddScoped<IUserDAO, UserDAO>();

builder.Services.AddScoped<UsersService>();
builder.Services.AddScoped<UsersFacade>();

builder.Services.AddSingleton<EmailNotificationStrategy>();
builder.Services.AddSingleton<SmsNotificationStrategy>();

builder.Services.AddSingleton<IUserObserver>(provider =>
    new EmailUserObserver(provider.GetRequiredService<EmailNotificationStrategy>()));
builder.Services.AddSingleton<IUserObserver>(provider =>
    new SmsUserObserver(provider.GetRequiredService<SmsNotificationStrategy>()));

builder.Services.AddSingleton<UserUpdateNotifier>(provider =>
{
    var notifier = new UserUpdateNotifier();

    foreach (var observer in provider.GetServices<IUserObserver>())
    {
        notifier.RegisterObserver(observer);
    }

    return notifier;
});

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