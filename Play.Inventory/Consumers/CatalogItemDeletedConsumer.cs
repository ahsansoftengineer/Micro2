using MassTransit;
using Play.Catalog.Contracts;
using Play.Common.Repo;
using Play.Inventory.Entities;

namespace Play.Inventory.Consumers;
public class CatalogItemDeletedConsumer : IConsumer<CatalogItemDeleted>
{
    private readonly IRepo<CatalogItem> repo;

    public CatalogItemDeletedConsumer(IRepo<CatalogItem> repo)
    {
        this.repo = repo;
    }

    public async Task Consume(ConsumeContext<CatalogItemDeleted> context)
    {
        var message = context.Message;
        var item = await repo.GetAsync(message.ItemId);

        if (item == null) return;
        await repo.RemoveAsync(message.ItemId);
    }
}