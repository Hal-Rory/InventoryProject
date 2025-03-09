using InventoryService.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryService.Databases;

public class InventoryDbContext: DbContext
{
	public DbSet<ItemBase> Item { get; set; }

	public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
}