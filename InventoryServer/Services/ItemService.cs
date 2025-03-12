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

	public async Task<bool> CreateItem(ItemBase item)
	{
		_context.Item.Add(item);
		int result = await _context.SaveChangesAsync();
		return result > 0;
	}

	public async Task<string> CreateDummyItems()
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
		return $"Created {itemsAdded} items";
	}

	public async Task<ItemBase?> GetItem(string itemId)
	{
		ItemBase? item = await _context.Item.FindAsync(itemId);
		return item ?? throw new KeyNotFoundException($"No item with id: {itemId} was found");
	}

	public async Task<List<ItemBase>> GetAllItems()
	{
		List<ItemBase> items = await _context.Item.ToListAsync();
		return items;
	}

	public async Task<bool> RemoveItem(string itemId)
	{
		ItemBase? item = await _context.Item.FindAsync(itemId);
		if (item == null) throw new KeyNotFoundException($"No item with id: {itemId} was found");
		_context.Item.Remove(item);
		int result = await _context.SaveChangesAsync();
		return result > 0;
	}
}