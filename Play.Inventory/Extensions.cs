using Play.Inventory.Dtos;
using Play.Inventory.Entities;
using Play.Inventory.Service.Clients;
using Polly;
using Polly.Timeout;

namespace Play.Inventory
{
    public static class Extensions
    {
        public static InventoryItemDto AsDto(this InventoryItem item, string name, string description)
        {
            return new InventoryItemDto(
                item.CatalogItemId,
                name,
                description,
                item.Quantity,
                item.AcquiredDate);
        }

        public static IServiceCollection AddTransientPolicy(this IServiceCollection services)
        {
            services.AddHttpClient<CatalogClient>(client =>
            {
                client.BaseAddress = new Uri("https://localhost:8000");
            })
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>()
                .WaitAndRetryAsync(5,
                    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                    onRetry: (OutcomeType, timeSpan, retryAttempt) => {
                        var sp = services.BuildServiceProvider();
                        sp.GetService<ILogger<CatalogClient>>()?
                        .LogWarning($"Delaying for {timeSpan.TotalSeconds} seconds, thenmaking retry {retryAttempt}");
                    }
            ))
            .AddTransientHttpErrorPolicy(builder => builder.Or<TimeoutRejectedException>()
                .CircuitBreakerAsync(3,
                    TimeSpan.FromSeconds(15),
                    onBreak: (OutcomeType, timeSpan) => {
                        var sp = services.BuildServiceProvider();
                        sp.GetService<ILogger<CatalogClient>>()?
                        .LogWarning($"Opening the Circuit for {timeSpan.TotalSeconds} seconds...");
                    },
                    onReset: () =>  {
                        var sp = services.BuildServiceProvider();
                        sp.GetService<ILogger<CatalogClient>>()?
                        .LogWarning($"Closing Circuit...");
                    }
            ))
            .AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
            return services;
        }
    }
}