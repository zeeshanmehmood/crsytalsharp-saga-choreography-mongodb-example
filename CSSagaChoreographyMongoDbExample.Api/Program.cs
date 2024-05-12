using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using CrystalSharp;
using CrystalSharp.EventStores.EventStoreDb.Extensions;
using CrystalSharp.MongoDb.Extensions;
using CSSagaChoreographyMongoDbExample.Application.OrderSaga.Transactions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string eventStoreConnectionString = builder.Configuration.GetConnectionString("AppEventStoreConnectionString");
string sagaStoreConnectionString = builder.Configuration.GetConnectionString("AppSagaStoreConnectionString");
string sagaStoreDatabase = "crystalsharp-sagastore-example";
MongoDbSettings sagaStoreDbSettings = new(sagaStoreConnectionString, sagaStoreDatabase);

CrystalSharpAdapter.New(builder.Services)
    .AddCqrs(typeof(PlaceOrderTransaction))
    .AddEventStoreDbEventStore<int>(eventStoreConnectionString)
    .AddMongoDbSagaStore(sagaStoreDbSettings, typeof(PlaceOrderTransaction))
    .CreateResolver();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
