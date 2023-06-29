using Play.Common.Repo;
using Play.Inventory.Entities;
using Play.Inventory.Service.Clients;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager? configuration = builder.Configuration;
IServiceCollection? services = builder.Services;

// Add services to the container.
services
  .AddMongo()
  .AddMongoRepo<InventoryItem>("inventoryItem");

services.AddHttpClient<CatalogClient>(client => {
  client.BaseAddress = new Uri("https://localhost:8000");
});

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