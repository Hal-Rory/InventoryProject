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

	[HttpPut("Create-Update/InventoryItem")]
	public async Task<IActionResult> CreateUpdateInventoryItem(InventoryItem item)
	{
		string? response = await _inventoryService.CreateUpdatePlayerItem(item);
		if(!string.IsNullOrEmpty(response)) return Conflict(response);
		return Ok("Item updated");
	}
	[HttpPost("Create/InventoryItem")]
	public async Task<IActionResult> CreateInventoryItem(InventoryItem item)
	{
		string? response = await _inventoryService.CreateItem(item);
		if(!string.IsNullOrEmpty(response)) return Conflict(response);
		return Ok("Item updated");
	}

	[HttpGet("Get-All/PlayerInventory")]
	public async Task<ActionResult<List<InventoryItem>>> GetAllPlayerInventory(int playerId)
	{
		(List<InventoryItem>? items, string error) = await _inventoryService.GetAllItems(playerId);
		if (items == null)
		{
			return BadRequest(new { message = error });
		}

		return Ok(items);
	}

	[HttpGet("Get/InventoryItem")]
	public async Task<ActionResult<InventoryItem>> GetInventoryItem(int playerId, string itemId)
	{
		InventoryItem? item = await _inventoryService.GetItem(playerId, itemId);
		if (item == null)
		{
			return NotFound();
		}
		return Ok(item);
	}

	[HttpPut("Update/InventoryItemQuantity")]
	public async Task<IActionResult> UpdateInventoryItem(InventoryItem item)
	{
		string? response = await _inventoryService.UpdatePlayerItemQuantity(item);
		if(!string.IsNullOrEmpty(response)) return Conflict(response);
		return Ok("Item updated");
	}

	[HttpPut("Remove/Item")]
	public async Task<IActionResult> RemoveInventoryItem(int playerId, string itemId)
	{
		string? response = await _inventoryService.RemoveInventoryItem(playerId, itemId);
		if(!string.IsNullOrEmpty(response)) return Conflict(response);
		return Ok("Item removed");
	}
}