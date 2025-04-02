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
	public async Task<IActionResult> CreateItem([FromBody] Item item)
	{
		bool success = await _itemService.CreateItem(item);
		return Ok($"Item{(success ? " " : " not ")}created");
	}

	[HttpGet("Get/Item")]
	public async Task<ActionResult<Item>> GetItem(string itemId)
	{
		Item? item = await _itemService.GetItem(itemId);
		return Ok(item);
	}

	[HttpGet("Get-All/Items")]
	public async Task<ActionResult<List<Item>>> GetAllItems()
    {
	    List<Item> items = await _itemService.GetAllItems();
	    return Ok(items);
    }

	[HttpPut("Update/Item")]
	public async Task<IActionResult> UpdateItem([FromBody] Item item)
	{
		bool success = await _itemService.UpdateItem(item);
		return success ? Ok("Item updated") : BadRequest("Item was not updated");
	}

	[HttpDelete("Remove/Item")]
	public async Task<IActionResult> DeleteItem(string itemId)
	{
		bool success = await _itemService.RemoveItem(itemId);
		return Ok($"Item{(success ? " " : "not ")}removed");
	}
}