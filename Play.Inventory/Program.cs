using Play.Common.MassTransit;
using Play.Common.Repo;
using Play.Inventory;
using Play.Inventory.Entities;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager? configuration = builder.Configuration;
IServiceCollection? services = builder.Services;

// Add services to the container.
services
  .AddMongo()
  .AddMongoRepo<InventoryItem>("inventoryItems")
  .AddMongoRepo<CatalogItem>("catalogItems")
  .AddMassTransitWithRabbitMq();

services.AddTransientPolicy();

services.AddControllers(options =>
{
  options.SuppressAsyncSuffixInActionNames = false;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
