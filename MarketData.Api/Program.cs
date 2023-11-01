#define UseEF
#define UseLocalDb
#if !UseEF
#define UseMongo
#endif
#if UseEF
using MarketData.Db.EF;
#endif
using MarketData.Db.Interfaces;
#if UseMongo
using MarketData.Db.Mongo;
#endif
using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
#if UseEF
    .AddDbContextPool<MarketDataDbContext>(
        optionBuilder => optionBuilder
            .UseSqlServer(
#if UseLocalDb
                builder.Configuration.GetConnectionString("LocalDb"), // NB: Swap for next line to use full SQL Server instance
#else
                builder.Configuration.GetConnectionString("SqlServer"),
#endif
                options => options.UseDateOnlyTimeOnly())
            .LogTo(Console.WriteLine))
    .AddScoped<IMarketDataProvider, EFMarketDataProvider>()
#endif
#if UseMongo
    .AddSingleton(MongoUrl.Create(builder.Configuration.GetConnectionString("MongoDb")))
    .AddSingleton(services => MongoClientSettings.FromUrl(services.GetRequiredService<MongoUrl>()))
    .AddSingleton(services => new MongoClient(services.GetRequiredService<MongoClientSettings>()))
    .AddSingleton(services => services.GetRequiredService<MongoClient>().GetDatabase(services.GetRequiredService<MongoUrl>().DatabaseName))
    .AddScoped<IMarketDataProvider, MongoMarketDataProvider>()
#endif
    ;

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.Run();