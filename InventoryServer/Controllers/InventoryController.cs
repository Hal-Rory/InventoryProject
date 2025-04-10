using InventoryProject.Helpers;
using InventoryProject.Models;
using InventoryProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryProject.Controllers;

[ApiController]
[Route($"[controller]/{HelperVariables.SwaggerVersion}")]
public class InventoryController : ControllerBase
{
	private readonly InventoryService _inventoryService;

	public InventoryController(InventoryService inventoryService)
	{
		_inventoryService = inventoryService;
	}

	/// <summary>
	/// Creates a new item if it doesn't exist but if it does,
	/// it modifies the existing one to update the quantity
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	[HttpPost("Create/PlayerItem")]
	public async Task<IActionResult> CreateUpdateInventoryItem([FromBody] InventoryItem item)
	{
		bool success = await _inventoryService.CreateUpdatePlayerItem(item);
		return Ok($"Item {(success ? "" : "not ")}created");
	}

	[HttpGet("Get/PlayerItem")]
	public async Task<ActionResult<PlayerItem>> GetPlayerItem(int playerId, string itemId)
	{
		PlayerItem item = await _inventoryService.GetItem(playerId, itemId);
		return Ok(item);
	}

	[HttpGet("Get-All/PlayerItems")]
	public async Task<ActionResult<List<PlayerItem>>> GetAllPlayerItems(int playerId)
	{
		List<PlayerItem> items = await _inventoryService.GetAllItems(playerId);
		return Ok(items);
	}

	[HttpPost("Update/PlayerItem")]
	public async Task<IActionResult> UpdateInventoryItem([FromBody] InventoryItem item)
	{
		await _inventoryService.UpdatePlayerItemQuantity(item);
		return Ok("Item updated");
	}

	[HttpPost("Update/PlayerItems")]
	public async Task<IActionResult> UpdateInventoryItems([FromBody] InventoryItem[] items)
	{
		await _inventoryService.SwapPlayerItems(items);
		return Ok("Item updated");
	}

	[HttpDelete("Remove/PlayerItem")]
	public async Task<IActionResult> RemoveInventoryItem(int playerId, string itemId)
	{
		await _inventoryService.RemoveInventoryItem(playerId, itemId);
		return Ok("Item removed");
	}
}