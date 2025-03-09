using InventoryService.Databases;
using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;

namespace InventoryService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
	private readonly InventoryDbContext _context;

	public ItemsController(InventoryDbContext context)
	{
		_context = context;
	}

	[HttpGet("{id}")]
	public async Task<ActionResult<ItemBase>> GetItem(string id)
	{
		ItemBase? item = await _context.Item.FindAsync(id);
		if (item == null)
		{
			return NotFound();
		}
		return Ok(item);
	}

	[HttpPost]
	public async Task<ActionResult<ItemBase>> CreateItem(ItemBase item)
	{
		_context.Item.Add(item);
		await _context.SaveChangesAsync();
		return CreatedAtAction(nameof(GetItem), new { id = item.ItemId }, item);
	}
}