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

	[HttpPost("Create/Item")]
	public async Task<IActionResult> CreateItem([FromBody] ItemBase item)
	{
		bool success = await _itemService.CreateItem(item);
		return Ok($"Item {(success ? "" : "not ")}created");
	}

	[HttpPost("Create/DummyItems")]
	public async Task<IActionResult> CreateDummyItems()
	{
		string results = await _itemService.CreateDummyItems();
		return Ok(results);
	}

	[HttpGet("Get-All/Items")]
	public async Task<ActionResult<List<ItemBase>>> GetAllItems()
    {
	    List<ItemBase> items = await _itemService.GetAllItems();
	    return Ok(items);
    }

	[HttpGet("Get/Item")]
	public async Task<ActionResult<ItemBase>> GetItem(string id)
	{
		ItemBase? item = await _itemService.GetItem(id);
		return Ok(item);
	}

	[HttpDelete("Remove/Item")]
	public async Task<IActionResult> DeleteItem(string itemId)
	{
		bool success = await _itemService.RemoveItem(itemId);
		return Ok($"Item{(success ? " " : "not ")}removed");
	}
}