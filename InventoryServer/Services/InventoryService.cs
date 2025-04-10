using System.Data;
using InventoryProject.Databases;
using InventoryProject.Helpers;
using InventoryProject.Models;
using Microsoft.Data.SqlClient;
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
		bool updatePlayerItem = await CheckPlayerItem(item.Player, item.Item);
		if (!updatePlayerItem)
		{
			throw new KeyNotFoundException($"No inventory item {item.Item} and/or player {item.Player} exists");
		}
		await _context.Database.ExecuteSqlRawAsync("EXEC SetInventory @p0, @p1, @p2", item.Player, item.Item, item.ItemQuantity);
		return true;
	}

	/// <summary>
	/// Get an item from the inventory list from the player
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task<PlayerItem> GetItem(int playerId, string itemId)
	{
		bool updatePlayerItem = await CheckPlayerItem(playerId, itemId);
		if (!updatePlayerItem)
		{
			throw new KeyNotFoundException($"No inventory item {itemId} and/or player {playerId} exists");
		}
		PlayerItem item = await _context.PlayerItem
			.FromSqlRaw("EXEC GetInventory @p0 @p1", playerId, itemId)
			.FirstAsync();
		return item ?? throw new KeyNotFoundException($"No inventory item {itemId} and/or player {playerId} exists");
	}

	/// <summary>
	/// Get all items from the inventory list for a player
	/// </summary>
	/// <param name="playerId"></param>
	/// <returns>A collection of item details to form the entire item</returns>
	public async Task<List<PlayerItem>> GetAllItems(int playerId)
	{
		bool updatePlayerItem = await CheckPlayerItem(playerId);
		if (!updatePlayerItem)
		{
			throw new KeyNotFoundException($"No player {playerId} exists");
		}
		List<PlayerItem> items = await _context.PlayerItem
			.FromSqlRaw("EXEC GetAllInventory @p0", playerId)
			.ToListAsync();
		return items;
	}

	/// <summary>
	/// Update the amount of an item
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public async Task UpdatePlayerItemQuantity(InventoryItem item)
	{
		bool updatePlayerItem = await CheckPlayerItem(item.Player, item.Item);
		if (!updatePlayerItem)
		{
			throw new KeyNotFoundException($"No inventory item {item.Item} and/or player {item.Player} exists");
		}

		InventoryItem updatedItem = await _context.Inventory
			.FirstAsync(i => i.Player == item.Player && i.Item == item.Item);

		updatedItem.ItemQuantity += item.ItemQuantity;

		await _context.SaveChangesAsync();
	}

	/// <summary>
	/// Updates multiple items
	/// </summary>
	/// <param name="items"></param>
	/// <returns></returns>
	public async Task SwapPlayerItems(InventoryItem[] items)
	{
		DataTable itemsTable = new DataTable();
		itemsTable.Columns.Add("Player_ID", typeof(int));
		itemsTable.Columns.Add("Item_ID", typeof(string));
		itemsTable.Columns.Add("ItemQuantity", typeof(int));

		foreach (InventoryItem item in items)
		{
			if (!await CheckPlayerItem(item.Player, item.Item))
				throw new KeyNotFoundException($"No inventory item {item.Item} and/or player {item.Player} exists");
			itemsTable.Rows.Add(item.Player, item.Item, item.ItemQuantity);
		}

		string paramName = "CraftItems";

		SqlParameter itemsTableParam = new SqlParameter($"@{paramName}", SqlDbType.Structured)
		{
			TypeName = "InventoryItems",
			Value = itemsTable
		};

		await _context.Database.ExecuteSqlRawAsync($"EXEC UpdateInventories @{paramName}", itemsTableParam);
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
	/// <param name="playerId"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	private async Task<bool> CheckPlayerItem(int playerId = -1, string itemId = "")
	{
		bool playerExists = playerId == -1 || await _context.Player.AnyAsync(p => p.PlayerId == playerId);
		bool itemExists = string.IsNullOrEmpty(itemId) || await _context.Item.AnyAsync(i => i.ItemId == itemId);
		return !(string.IsNullOrEmpty(itemId) && playerId == -1) && playerExists && itemExists;
	}
}