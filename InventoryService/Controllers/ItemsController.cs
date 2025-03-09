using InventoryService.Databases;
using InventoryService.Helpers;
using InventoryService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
	private readonly InventoryDbContext _context;

	public ItemsController(InventoryDbContext context)
	{
		_context = context;
	}
	[HttpGet("Get All")]
	public async Task<IActionResult> GetAllItemsAsync()
    {
        try
        {
            List<ItemBase> items = await _context.Item.ToListAsync();
            return Ok(items);
        }
        catch (Exception e)
        {
            return BadRequest(new { message = "An error occurred while retrieving items.", error = e.Message });
        }
    }

	[HttpGet("Get Item")]
	public async Task<ActionResult<ItemBase>> GetItem(string id)
	{
		ItemBase? item = await _context.Item.FindAsync(id);
		if (item == null)
		{
			return NotFound();
		}
		return Ok(item);
	}

	[HttpPost("Create Item")]
	public async Task<IActionResult> CreateItem(ItemBase item)
	{
		try
		{
			_context.Item.Add(item);
			int result = await _context.SaveChangesAsync();
			if (result <= 0)
			{
				_context.Item.Remove(item);
				return Conflict($"Item could not be added: {item.ItemId}");
			}
		}
		catch (DbUpdateException dbue)
		{
			return Conflict(new {message = $"Item could not be added: {item.ItemId}", details = dbue.Message});
		}

		return Ok(item);
	}

	[HttpPost("Create Dummy Items")]
	public async Task<IActionResult> CreateDummyItems()
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
		return Ok($"Items added: {itemsAdded}");
	}
}