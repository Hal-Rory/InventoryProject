using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Models;

[PrimaryKey(nameof(Player),nameof(Item))]
public class InventoryItem
{
	[ForeignKey(HelperVariables.PlayerId)]
	[Column(HelperVariables.PlayerId)]
	public int Player { get; set;}

	[ForeignKey(HelperVariables.ItemId)]
	[Column(HelperVariables.ItemId)]
	public string Item { get; set; } = "";

	public int ItemQuantity { get; set; }
}