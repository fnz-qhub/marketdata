#define UseEF
#if !UseEF
#define UseMongo
#endif
#if UseEF
using MarketData.Db.EF;
using MarketData.Db.EF.Entities;
#endif
using MarketData.Db.Interfaces;
#if UseMongo
using MarketData.Db.Mongo;
using MarketData.Db.Mongo.Entities;
#endif
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
#if UseEF
    .AddDbContextPool<MarketDataDbContext>(
        builder => builder
            .UseSqlServer(
                @"Server=(localdb)\mssqllocaldb; Database=MarketData; Trusted_Connection=True; MultipleActiveResultSets=true",
                options => options.UseDateOnlyTimeOnly())
            .LogTo(Console.WriteLine))
    .AddScoped<IMarketDataProvider<FundPrice>, EFMarketDataProvider>()
#endif
#if UseMongo
    .AddScoped<IMarketDataProvider<FundPrice>, MongoMarketDataProvider>()
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