using Presentation;
using Infrastructure.UnitOfWork;
using Infrastructure.Persistence.MongoDb;
using Microsoft.AspNetCore.Cors.Infrastructure;
using MongoDB.Driver;
using Presentation.Middleware;
using Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var defaultCorsPolicy = "default_cors_policy";

// Add services to the container.

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(options =>
    {
        //prevent automatic 400-404 response
        options.SuppressModelStateInvalidFilter = true;
        options.SuppressMapClientErrors = true;
    });

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddConfigSections(builder.Configuration);

builder.Services.AddSingleton<IMongoClient>(_ =>
    new MongoClient(builder.Configuration["MongoDbConfig:ConnectionString"])
);

builder.Services.AddScoped<IMongoContext, MongoContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddApplicationServices();

//configure all mappings
builder.Services.AddMappings();

builder.Services.AddCors(config =>
{
    var policy = new CorsPolicy();
    policy.Headers.Add("*");
    policy.Methods.Add("*");
    policy.Origins.Add("*");
    //policy.SupportsCredentials = true;
    config.AddPolicy(defaultCorsPolicy, policy);
});

builder.Services.ConfigureJwtAuthentication();
builder.Services.ConfigureJwtAuthorization();

var app = builder.Build();


await using (var scope = app.Services.CreateAsyncScope())
{
    await MongoDbPersistence.ConfigureAsync(scope.ServiceProvider.GetRequiredService<IMongoContext>());
} 

app.UseMiddleware<ErrorHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();
app.UseMiddleware<TenantMiddleware>();

app.MapControllers();

app.Run();
