using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;

namespace InventoryProject.Models;
public class Item
{
	[Key]
 	[Column(HelperVariables.ItemId)]
 	public string ItemId { get; set; } = "";
 	public string ItemDescription { get; set; } = "";
}