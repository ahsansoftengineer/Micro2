using Microsoft.AspNetCore.Mvc;
using Play.Common.Repo;
using Play.Inventory.Dtos;
using Play.Inventory.Entities;
using Play.Inventory.Service.Clients;

namespace Play.Inventory.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ItemsController : ControllerBase
    {
        private readonly ILogger<ItemsController> _logger;
        private readonly IRepo<InventoryItem> repo;
        private readonly CatalogClient client;


        public ItemsController(
            ILogger<ItemsController> logger,
            IRepo<InventoryItem> repo,
            CatalogClient client
            )
        {
            _logger = logger;
            this.repo = repo;
            this.client = client;

        }
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

        [HttpPost]

        public async Task<ActionResult> PostAsync(GrantItemsDto data)
        {
            InventoryItem? itemz = await repo.GetAsync(
                item => item.UserId == data.UserId &&
                item.CatalogItemId == data.CatalogItemId);
            if (itemz == null)
            {
                itemz = new InventoryItem
                {
                    CatalogItemId = data.CatalogItemId,
                    UserId = data.UserId,
                    Quantity = data.Quantity,
                    AcquiredDate = DateTimeOffset.UtcNow
                };
                await repo.CreateAsync(itemz);

            }
            else
            {
                itemz.Quantity += data.Quantity;
                await repo.UpdateAsync(itemz);
            }
            return Ok();
        }
    }
}