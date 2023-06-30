## Inter Service Communication
```c#

services.AddHttpClient<CatalogClient>(client => {
  client.BaseAddress = new Uri("https://localhost:8000");
});

[HttpGet("{userId}")]
public async Task<ActionResult<IEnumerable<InventoryItem>>> GetAsync(Guid userId)
{
    if (userId == Guid.Empty) return BadRequest();

    var catalogItems = await client.GetCatalogItemsAsync();

    var inventoryitemEntities = await repo.GetAllAsync(items => items.UserId == userId);

    var inventoryItemDtos = inventoryitemEntities.Select(inventoryItem =>
    {
        var catalogItem = catalogItems.Single(items => items.Id == inventoryItem.CatalogItemId);

        return inventoryItem.AsDto(catalogItem.Name, catalogItem.Description);

    });

    return Ok(inventoryItemDtos);
}
```

## Adding Polly Service

```c#
dotnet add package Microsoft.Extensions.Http.Polly

services.AddHttpClient<CatalogClient>(client => {
  client.BaseAddress = new Uri("https://localhost:8000");
})
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));
```
## Adding Polly with Service

```c#
services.AddHttpClient<CatalogClient>(client =>
{
  client.BaseAddress = new Uri("https://localhost:8000");
})
.AddTransientHttpErrorPolicy(builder =>
{
  return builder.Or<TimeoutRejectedException>().WaitAndRetryAsync(
    5,
    retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
    onRetry: (OutcomeType, timeSpan, retryAttempt) =>
    {
      var sp = services.BuildServiceProvider();
      sp.GetService<ILogger<CatalogClient>>()?
        .LogWarning($"Delaying for {timeSpan.TotalSeconds} seconds, thenmaking retry {retryAttempt}");
    }

  );
})
.AddPolicyHandler(Policy.TimeoutAsync<HttpResponseMessage>(1));


dotnet run --launch-profile https
```

