using InventoryProject.Databases;
using InventoryProject.Helpers;
using InventoryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Services;

public class InventoryService
{
	private readonly InventoryDbContext _context;

	public InventoryService(InventoryDbContext context)
	{
		_context = context;
	}

	/// <summary>
	/// Create or overwrite an inventory object
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public async Task<bool> CreateUpdatePlayerItem(InventoryItem item)
	{
		bool updatePlayerItem = await CheckPlayerItem(item);
		if (!updatePlayerItem)
		{
			throw new KeyNotFoundException($"No inventory item {item.Item} and/or player {item.Player} exists");
		}
		await _context.Database.ExecuteSqlRawAsync("EXEC UpdateInventory @p0, @p1, @p2", item.Player, item.Item, item.ItemQuantity);
		return true;
	}

	/// <summary>
	/// Get an item from the inventory list from the player
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task<InventoryItem> GetItem(int playerId, string itemId)
	{
		InventoryItem? item = await _context.Inventory.SingleAsync(item =>
			item.Player == playerId && item.Item == itemId && item.ItemQuantity > 0);
		return item ?? throw new KeyNotFoundException($"No inventory item {itemId} and/or player {playerId} exists");
	}

	/// <summary>
	/// Get all items from the inventory list for a player
	/// </summary>
	/// <param name="playerId"></param>
	/// <returns></returns>
	public async Task<List<InventoryItem>> GetAllItems(int playerId = -1)
	{
		List<InventoryItem> items;

		if (playerId < 0)
		{
			items = await _context.Inventory.ToListAsync();
		}
		else
		{
			items = await _context.Inventory
				.Where(item => item.Player == playerId && item.ItemQuantity > 0)
				.ToListAsync();
		}
		return items;
	}

	/// <summary>
	/// Update the amount of an item
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public async Task UpdatePlayerItemQuantity(InventoryItem item)
	{
		bool updatePlayerItem = await CheckPlayerItem(item);
		if (!updatePlayerItem)
		{
			throw new KeyNotFoundException($"No inventory item {item.Item} and/or player {item.Player} exists");
		}

		InventoryItem updatedItem = await _context.Inventory
			.FirstAsync(i => i.Player == item.Player && i.Item == item.Item);

		updatedItem.ItemQuantity = item.ItemQuantity;

		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Clear the quantity for an item to "remove" it
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task RemoveInventoryItem(int playerId, string itemId)
	{
		await UpdatePlayerItemQuantity(new InventoryItem{Player = playerId, Item = itemId, ItemQuantity = 0});
	}

	/// <summary>
	/// Check that the player and item exist
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	private async Task<bool> CheckPlayerItem(InventoryItem item)
	{
		var playerExists = await _context.Player.AnyAsync(p => p.PlayerId == item.Player);
		var itemExists = await _context.Item.AnyAsync(i => i.ItemId == item.Item);
		return playerExists && itemExists;
	}
}