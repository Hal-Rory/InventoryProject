using InventoryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Databases;

public class InventoryDbContext: DbContext
{
	public DbSet<Item> Item { get; set; }
	public DbSet<Player> Player { get; set; }

	public DbSet<InventoryItem> Inventory { get; set; }

	public DbSet<PlayerItem> PlayerItem { get; set; }

	public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
}