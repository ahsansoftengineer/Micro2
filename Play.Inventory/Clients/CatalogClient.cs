using System.Net.Http;
using Play.Inventory.Dtos;

namespace Play.Inventory.Service.Clients {
  public class CatalogClient {

    private readonly HttpClient httpClient;
    public CatalogClient(HttpClient httpClient)
    {
      this.httpClient = httpClient;
    }

    public async Task<IReadOnlyCollection<CatalogItemDto>> GetCatalogItemsAsync() {
      IReadOnlyCollection<CatalogItemDto>? items = await httpClient.GetFromJsonAsync<IReadOnlyCollection<CatalogItemDto>>("/items");
      return items;
    }
  }
}