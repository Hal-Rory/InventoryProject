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
	/// Get an item from the inventory list from the player
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task<InventoryItem?> GetItem(int playerId, string itemId)
	{
		return await _context.Inventory.SingleOrDefaultAsync(item =>
			item.Player == playerId && item.Item == itemId && item.ItemQuantity > 0);
	}

	/// <summary>
	/// Get all items from the inventory list for a player
	/// </summary>
	/// <param name="playerId"></param>
	/// <returns></returns>
	public async Task<(List<InventoryItem>?, string)> GetAllItems(int playerId)
	{
		try
		{
			List<InventoryItem> items = await _context.Inventory
				.Where(item => item.Player == playerId && item.ItemQuantity > 0)
				.ToListAsync();
			return (items, string.Empty);
		}
		catch (Exception e)
		{
			return (null, $"An error occurred while retrieving items. {e.Message}" );
		}
	}

	/// <summary>
	/// Create an inventory item entry (Use if absolutely certain there is not an item there already)
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public async Task<string?> CreateItem(InventoryItem item)
	{
		try
		{
			_context.Inventory.Add(item);
			int result = await _context.SaveChangesAsync();
			if (result <= 0)
			{
				_context.Inventory.Remove(item);
				return $"Item could not be added: {item.Item}";
			}
		}
		catch (DbUpdateException dbue)
		{
			return $"Item could not be added: {item.Item}\n{dbue.Message}";
		}

		return null;
	}

	/// <summary>
	/// Use if it is unclear if this item exists already(such as an item with a quantity of 0)
	/// It is additive.
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public async Task<string?> CreateUpdatePlayerItem(InventoryItem item)
	{
		try
		{
			string? updatePlayerItem = await CheckPlayerItem(item);
			if (!string.IsNullOrEmpty(updatePlayerItem)) return updatePlayerItem;

			int result = await _context.Database.ExecuteSqlRawAsync("EXEC UpdateInventory @p0, @p1, @p2", item.Player, item.Item, item.ItemQuantity);
			if (result <= 0)
			{
				return $"{item.Player} did not receive {item.Item}";
			}
		}
		catch (DbUpdateException dbue)
		{
			return $"{item.Player} did not receive {item.Item}\n{dbue.Message}";
		}
		return null;
	}

	/// <summary>
	/// Update the amount of an item
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	public async Task<string?> UpdatePlayerItemQuantity(InventoryItem item)
	{
		try
		{
			string? updatePlayerItem = await CheckPlayerItem(item);
			if (!string.IsNullOrEmpty(updatePlayerItem)) return updatePlayerItem;

			InventoryItem? updatedItem = await _context.Inventory
				.FirstOrDefaultAsync(i => i.Player == item.Player && i.Item == item.Item);

			if (updatedItem == null) return "Item not found in inventory.";

			updatedItem.ItemQuantity = item.ItemQuantity;

			await _context.SaveChangesAsync();

			return null;
		}
		catch (Exception ex)
		{
			return $"An error occurred while removing the item.\n{ex.Message }";
		}
	}

	/// <summary>
	/// Clear the quantity for an item to "remove" it
	/// </summary>
	/// <param name="playerId"></param>
	/// <param name="itemId"></param>
	/// <returns></returns>
	public async Task<string?> RemoveInventoryItem(int playerId, string itemId)
	{
		return await UpdatePlayerItemQuantity(new InventoryItem{Player = playerId, Item = itemId, ItemQuantity = 0});
	}

	/// <summary>
	/// Check that the player and item exist
	/// </summary>
	/// <param name="item"></param>
	/// <returns></returns>
	private async Task<string?> CheckPlayerItem(InventoryItem item)
	{
		var playerExists = await _context.Player.AnyAsync(p => p.PlayerId == item.Player);
		var itemExists = await _context.Item.AnyAsync(i => i.ItemId == item.Item);

		if (!playerExists || !itemExists)
		{
			return $"{(!playerExists ? "Player not found." : "")} {(!itemExists ? "Item not found." : "")}";
		}

		return null;
	}
}