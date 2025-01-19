using MassTransit;
using Microservice.Service2;
using Microservices.Service2.Consumers;
using Microservices.Service2.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSingleton(new SingltoneDatabase());
builder.Services.AddMassTransit(busConfiguration =>
{
    busConfiguration.SetKebabCaseEndpointNameFormatter();

    busConfiguration.AddConsumer<AddUserConsumer>();
    busConfiguration.AddConsumer<GetUsersConsumer>();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/get-users", (SingltoneDatabase db) =>
{
    return db.GetUsersEvents;
});

app.MapGet("/add-users", (SingltoneDatabase db) =>
{
    return db.AddUserEvents;
});
app.Run();
