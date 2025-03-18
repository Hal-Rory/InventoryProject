using InventoryProject.Databases;
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

	public async Task<bool> CreateItem(Item item)
	{
		_context.Item.Add(item);
		int result = await _context.SaveChangesAsync();
		return result > 0;
	}

	public async Task<Item?> GetItem(string itemId)
	{
		Item? item = await _context.Item.FindAsync(itemId);
		return item ?? throw new KeyNotFoundException($"No item with id: {itemId} was found");
	}

	public async Task<List<Item>> GetAllItems()
	{
		List<Item> items = await _context.Item.ToListAsync();
		return items;
	}

	public async Task<bool> RemoveItem(string itemId)
	{
		Item? item = await _context.Item.FindAsync(itemId);
		if (item == null) throw new KeyNotFoundException($"No item with id: {itemId} was found");
		_context.Item.Remove(item);
		int result = await _context.SaveChangesAsync();
		return result > 0;
	}
}