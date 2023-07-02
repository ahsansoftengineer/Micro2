using MassTransit;
using Play.Catalog.Contracts;
using Play.Catalog.Service.Entities;
using Play.Common.Repo;

namespace Play.Inventory.Service.Consumers;
public class CatalogItemCreatedConsumer : IConsumer<CatalogItemCreated>
{
    private readonly IRepo<CatalogItem> repo;

    public CatalogItemCreatedConsumer(IRepo<CatalogItem> repo)
    {
        this.repo = repo;
    }

    public async Task Consume(ConsumeContext<CatalogItemCreated> context)
    {
        var message = context.Message;
        var item = await repo.GetAsync(message.ItemId);

        if(item != null) return ;

        item = new CatalogItem {
          Id = message.ItemId,
          Name = message.Name,
          Description = message.Description
        };

        await repo.CreateAsync(item);
    }
}