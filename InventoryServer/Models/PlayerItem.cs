using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Models;
[Keyless]
public class PlayerItem
{
	public string Item_ID { get; set; } = "";
	public string ItemDescription { get; set; } = "";
	public int ItemQuantity { get; set; } = 0;
}