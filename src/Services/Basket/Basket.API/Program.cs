using Basket.API.Repositories;
using Basket.API.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Redis Configuration
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetValue<string>("CacheSettings:ConnectionString");
});

// // General Configuration
builder.Services.AddScoped<IBasketRepository, BasketRepository>();
// builder.Services.AddAutoMapper(typeof(Startup));

// // Grpc Configuration
// builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>
//             (o => o.Address = new Uri(Configuration["GrpcSettings:DiscountUrl"]));
// builder.Services.AddScoped<DiscountGrpcService>();

// // MassTransit-RabbitMQ Configuration
// builder.Services.AddMassTransit(config =>
// {
//     config.UsingRabbitMq((ctx, cfg) =>
//     {
//         cfg.Host(Configuration["EventBusSettings:HostAddress"]);
//         cfg.UseHealthCheck(ctx);
//     });
// });
// builder.Services.AddMassTransitHostedService();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();