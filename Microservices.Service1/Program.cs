using MassTransit;
using Microservices.Common.Contracts;
using Microservices.Service1.Consumers;
using Microservices.Service1.Data;
using Microservices.Service1.Data.Entities;
using Microservices.Service1.Options;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

string connectionString = builder.Configuration.GetConnectionString("DefaultPostgres") ?? throw new Exception("No connection string");
//var mySqlVersion = new MySqlServerVersion(new Version(8, 0, 40));
//builder.Services.AddDbContext<ApplicationDbContext>(o => o.UseMySql(connectionString, mySqlVersion));
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
builder.Services.AddMassTransit(busConfiguration =>
{
    busConfiguration.SetKebabCaseEndpointNameFormatter();

    busConfiguration.UsingRabbitMq((context, config) =>
    {
        RabbitMq options = builder.Configuration.GetSection("RabbitMq").Get<RabbitMq>() ?? throw new Exception("No RabbitMq section");
        config.Host(options.Host, h =>
        {
            h.Username(options.Username);
            h.Password(options.Password);
        });
        config.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/users", async (UserDto userDto, ApplicationDbContext dbContext, IPublishEndpoint publishEndpoint) =>
{
    dbContext.Users.Add(new User() { Firstname = userDto.Firstname, Surname = userDto.Surname});
    AddUserEvent addUserEvent = new(userDto.Firstname, userDto.Surname);
    await publishEndpoint.Publish(addUserEvent);
    await dbContext.SaveChangesAsync();
});
app.MapGet("/users", async (ApplicationDbContext db, IPublishEndpoint publishEndpoint) =>
{
    GetUsersEvent getUsersEvent = new GetUsersEvent();
    await publishEndpoint.Publish(getUsersEvent);
    return await db.Users.AsNoTracking().ToListAsync();
});
app.Run();

public record UserDto(string Firstname, string Surname);