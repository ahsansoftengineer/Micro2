using MassTransit;
using Play.Catalog.Contracts;
using Play.Common.Repo;
using Play.Inventory.Entities;

namespace Play.Inventory.Consumers;
public class CatalogItemUpdatedConsumer : IConsumer<CatalogItemUpdated>
{
    private readonly IRepo<CatalogItem> repo;

    public CatalogItemUpdatedConsumer(IRepo<CatalogItem> repo)
    {
        this.repo = repo;
    }

    public async Task Consume(ConsumeContext<CatalogItemUpdated> context)
    {
        var message = context.Message;
        var item = await repo.GetAsync(message.ItemId);

        if (item != null)
        {
            item = new CatalogItem
            {
                Id = message.ItemId,
                Name = message.Name,
                Description = message.Description
            };
            await repo.CreateAsync(item);
        }
        else
        {
            item.Name = message.Name;
            item.Description = message.Description;
            await repo.UpdateAsync(item);
        }
    }
}