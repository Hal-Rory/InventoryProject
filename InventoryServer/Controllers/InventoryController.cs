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
	public async Task<IActionResult> CreateUpdateInventoryItem(InventoryItem item)
	{
		bool success = await _inventoryService.CreateUpdatePlayerItem(item);
		return Ok($"Item {(success ? "" : "not ")}created");
	}

	[HttpGet("Get-All/SinglePlayerItem")]
	public async Task<ActionResult<List<InventoryItem>>> GetAllPlayerItems(int playerId)
	{
		List<InventoryItem> items = await _inventoryService.GetAllItems(playerId);
		return Ok(items);
	}

	[HttpGet("Get/PlayerItem")]
	public async Task<ActionResult<InventoryItem>> GetPlayerItem(int playerId, string itemId)
	{
		InventoryItem item = await _inventoryService.GetItem(playerId, itemId);
		return Ok(item);
	}

	[HttpPut("Update/PlayerItem")]
	public async Task<IActionResult> UpdateInventoryItem(InventoryItem item)
	{
		await _inventoryService.UpdatePlayerItemQuantity(item);
		return Ok("Item updated");
	}

	[HttpDelete("Remove/PlayerItem")]
	public async Task<IActionResult> RemoveInventoryItem(int playerId, string itemId)
	{
		await _inventoryService.RemoveInventoryItem(playerId, itemId);
		return Ok("Item removed");
	}
}