using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }
    private static readonly List<ItemDto> items = new()
    {
        new ItemDto(Guid.NewGuid(), "Potion", "Restores a small amount of HP", 5, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Antidote", "RCures Poision", 7, DateTimeOffset.UtcNow),
        new ItemDto(Guid.NewGuid(), "Bronze Sword", "Deals a small amount of damage", 20, DateTimeOffset.UtcNow),
    };

    [HttpGet]
    public IEnumerable<ItemDto> Get()
    {
        return items;
    }

    [HttpGet("{id}")]
    public ItemDto GetById(Guid id)
    {
        var item = items.Where(x => x.Id == id).SingleOrDefault();
        return item;
    }
    [HttpPost]
    public ActionResult<ItemDto> Post(CreateItemDto dto){
        var item = new ItemDto(Guid.NewGuid(), dto.Name, dto.Description, dto.Price, DateTimeOffset.UtcNow);
        items.Add(item);
        return CreatedAtAction(nameof(GetById), new {id = item.Id}, item);
    }
    [HttpPut("{id}")]
    public ActionResult Put(Guid id, CreateItemDto dto){
        var item = items.Where(item => item.Id == id).SingleOrDefault();

        var updatedItem = item with {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price
        } ;
        var index = items.FindIndex(x => x.Id == id);
        items[index] = updatedItem;
        return NoContent();
    }
    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id){
        var index = items.FindIndex(x => x.Id == id);
        items.RemoveAt(index);
        return NoContent();
    }

}
