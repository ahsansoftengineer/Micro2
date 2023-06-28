using Microsoft.AspNetCore.Mvc;
using Play.Catalog.Service.Dtos;
using Play.Catalog.Service.Entities;
using Play.Common.Repo;

namespace Play.Catalog.Service.Controllers;

[ApiController]
[Route("[controller]")]
public class ItemsController : ControllerBase
{
    private readonly ILogger<ItemsController> _logger;
    private readonly IRepo<Item> repo;
    public ItemsController(
        ILogger<ItemsController> logger,
        IRepo<Item> repo
        )
    {
        _logger = logger;
        this.repo = repo;
    }

    [HttpGet]
    public async Task<IEnumerable<ItemDto>> GetAsync()
    {
        var items = (await repo.GetAllAsync()).Select(
            item => item.AsDto()
        );
        return items;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ItemDto>> GetByIdAsync(Guid id)
    {
        var item  = await repo.GetAsync(id);

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
        await repo.CreateAsync(item);
        return CreatedAtAction(nameof(GetByIdAsync), new {id = item.Id}, item);
    }
    [HttpPut("{id}")]
    public async Task<ActionResult> Put(Guid id, CreateItemDto dto){
        var item = await repo.GetAsync(id);
        if(item == null) return NotFound();

        item.Name = dto.Name;
        item.Description = dto.Description;
        item.Price = dto.Price;

        await repo.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id){
        var item = await repo.GetAsync(id);
        if(item == null) return NotFound();

        await repo.RemoveAsync(id);
        return NoContent();
    }

}
