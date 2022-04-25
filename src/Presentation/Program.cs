using Presentation;
using Infrastructure.UnitOfWork;
using Infrastructure.Persistence.MongoDb;
using MongoDB.Driver;
using Presentation.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

var app = builder.Build();


await using (var scope = app.Services.CreateAsyncScope())
{
    MongoDbPersistence.ConfigureAsync(scope.ServiceProvider.GetRequiredService<IMongoContext>());
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

app.MapControllers();

app.Run();
