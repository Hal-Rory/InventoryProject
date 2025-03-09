using InventoryService.Models;

namespace InventoryService.Helpers;

public class ItemCreation
{
	public List<FoodItem> CreateFood()
	{
		// Create 5 WeaponItem instances
		List<FoodItem> foodItems = new List<FoodItem>
		{
			new FoodItem
			{
				ItemName = "Apple",
				ItemID = "AP001",
				Calories = 95,
				ExpirationDate = "2025-12-31",
				IsConsumable = true
			},
			new FoodItem
			{
				ItemName = "Banana",
				ItemID = "BN001",
				Calories = 105,
				ExpirationDate = "2025-12-30",
				IsConsumable = true
			},
			new FoodItem
			{
				ItemName = "Carrot",
				ItemID = "CR001",
				Calories = 25,
				ExpirationDate = "2025-01-15",
				IsConsumable = true
			},
			new FoodItem
			{
				ItemName = "Bread",
				ItemID = "BR001",
				Calories = 80,
				ExpirationDate = "2025-02-20",
				IsConsumable = true
			},
			new FoodItem
			{
				ItemName = "Cheese",
				ItemID = "CH001",
				Calories = 110,
				ExpirationDate = "2025-04-10",
				IsConsumable = true
			}
		};
		return foodItems;
	}
	public List<WeaponItem> CreateWeapons()
	{
		// Create 5 WeaponItem instances
		List<WeaponItem> weaponItems = new List<WeaponItem>
		{
			new WeaponItem
			{
				ItemName = "Longbow",
				ItemID = "LB001",
				Damage = 35,
				Durability = 150,
				IsRanged = true
			},
			new WeaponItem
			{
				ItemName = "Sword",
				ItemID = "SW001",
				Damage = 50,
				Durability = 100,
				IsRanged = false
			},
			new WeaponItem
			{
				ItemName = "Crossbow",
				ItemID = "CB001",
				Damage = 40,
				Durability = 120,
				IsRanged = true
			},
			new WeaponItem
			{
				ItemName = "Dagger",
				ItemID = "DG001",
				Damage = 20,
				Durability = 80,
				IsRanged = false
			},
			new WeaponItem
			{
				ItemName = "Magic Staff",
				ItemID = "MS001",
				Damage = 60,
				Durability = 200,
				IsRanged = true
			}
		};
		return weaponItems;
	}
}