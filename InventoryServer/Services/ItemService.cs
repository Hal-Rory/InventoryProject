using InventoryProject.Databases;
using InventoryProject.Helpers;
using InventoryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Services;

public class ItemService
{
	private readonly InventoryDbContext _context;

	public ItemService(InventoryDbContext context)
	{
		_context = context;
	}

	public async Task<ItemBase?> GetItem(string itemId)
	{
		return await _context.Item.FindAsync(itemId);
	}

	public async Task<(List<ItemBase>?, string)> GetAllItems()
	{
		try
		{
			List<ItemBase> items = await _context.Item.ToListAsync();
			return (items, string.Empty);
		}
		catch (Exception e)
		{
			return (null, $"An error occurred while retrieving items. {e.Message}" );
		}
	}

	/// <summary>
	/// Try creating an item
	/// </summary>
	/// <param name="item"></param>
	/// <returns>a response from the creation process, will return null on success</returns>
	public async Task<string?> CreateItem(ItemBase item)
	{
		try
		{
			_context.Item.Add(item);
			int result = await _context.SaveChangesAsync();
			if (result <= 0)
			{
				_context.Item.Remove(item);
				return $"Item could not be added: {item.ItemId}";
			}
		}
		catch (DbUpdateException dbue)
		{
			return $"Item could not be added: {item.ItemId}\n{dbue.Message}";
		}

		return null;
	}

	public async Task<int> CreateDummyItems()
	{
		ItemCreation itemCreation = new ItemCreation();
		int itemsAdded = 0;
		foreach (FoodItem foodItem in itemCreation.CreateFood())
		{
			if (_context.Item.Any(i => i.ItemId == foodItem.ItemID)) continue;
			await CreateItem(new ItemBase{ItemId = foodItem.ItemID, ItemType = "FoodItem", ItemDescription = foodItem.SerializeItem()});
			itemsAdded++;
		}
		foreach (WeaponItem weaponItem in itemCreation.CreateWeapons())
		{
			if (_context.Item.Any(i => i.ItemId == weaponItem.ItemID)) continue;
			await CreateItem(new ItemBase{ItemId = weaponItem.ItemID, ItemType = "WeaponItem", ItemDescription = weaponItem.SerializeItem()});
			itemsAdded++;
		}
		return itemsAdded;
	}
}