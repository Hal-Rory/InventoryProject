using InventoryProject.Models;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Databases;

public class InventoryDbContext: DbContext
{
	public DbSet<ItemBase> Item { get; set; }

	public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }
}