
using Play.Catalog.Service.Entities;
using Play.Common.MassTransit;
using Play.Common.Repo;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);
ConfigurationManager? configuration = builder.Configuration;
IServiceCollection? services = builder.Services;

// Add services to the container.
services
  .AddMongo()
  .AddMongoRepo<Item>("items");

services.AddMassTransitWithRabbitMq();

services.AddControllers(options =>
{
  options.SuppressAsyncSuffixInActionNames = false;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}
// Host.CreateDefaultBuilder(args)
//   .ConfigureWebHostDefaults(webBuilder =>
//   {
//       webBuilder.UseUrls("http://0.0.0.0:5001"); // Set the desired listening address and port
//   });

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
