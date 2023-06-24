using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Catalog.Service.Repo;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly ItemsRepository itemsRepository = new();
    public ItemsController(ILogger<ItemsController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetAsync()
    {
        var items = (await itemsRepository.GetAllAsync()).Select(
            item => item.AsDto()
        );
        return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
        var item  = await itemsRepository.GetAsync(id);

        if(item == null) return NotFound();
        return item.AsDto();
    }
    [HttpPost]
    public async Task<ActionResult<ItemDto>> PostAsync(CreateItemDto dto){
        var item = new Item {
            Name = dto.Name,
            Description = dto.Description,
            Price = dto.Price,
            CreatedDate = DateTimeOffset.UtcNow
        };
        await itemsRepository.CreateAsync(item);
        return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, CreateItemDto dto){
        var item = await itemsRepository.GetAsync(id);
        if(item == null) return NotFound();

        item.Name = dto.Name;
        item.Description = dto.Description;
        item.Price = dto.Price;

        await itemsRepository.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id){
        var item = await itemsRepository.GetAsync(id);
        if(item == null) return NotFound();

        await itemsRepository.RemoveAsync(id);
        return NoContent();
    }

}
