using Play.Inventory.Dtos;
using Play.Inventory.Entities;

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
    }
}