using InventoryProject.Helpers;
using InventoryProject.Models;
using InventoryProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryProject.Controllers;

[ApiController]
[Route($"[controller]/{HelperVariables.SwaggerVersion}")]
public class ItemsController : ControllerBase
{
	private readonly ItemService _itemService;

	public ItemsController(ItemService itemService)
	{
		_itemService = itemService;
	}
	[HttpGet("Get All Items")]
	public async Task<ActionResult<List<ItemBase>>> GetAllItems()
    {
	    var (items, error) = await _itemService.GetAllItems();

	    if (items == null)
	    {
		    return BadRequest(new { message = error });
	    }

	    return Ok(items);
    }

	[HttpGet("Get Item")]
	public async Task<ActionResult<ItemBase>> GetItem(string id)
	{
		ItemBase? item = await _itemService.GetItem(id);
		if (item == null)
		{
			return NotFound();
		}
		return Ok(item);
	}

	[HttpPost("Create Item")]
	public async Task<IActionResult> CreateItem(ItemBase item)
	{
		string? response = await _itemService.CreateItem(item);
		if(!string.IsNullOrEmpty(response)) return Conflict(response);
		return Ok("Item created");
	}

	[HttpPost("Create Dummy Items")]
	public async Task<IActionResult> CreateDummyItems()
	{
		int itemsAdded = await _itemService.CreateDummyItems();
		return Ok($"Items added: {itemsAdded}");
	}
}