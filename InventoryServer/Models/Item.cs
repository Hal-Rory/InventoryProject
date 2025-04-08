using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;

namespace InventoryProject.Models;
public class Item
{
	/// <summary>
	/// This converts the naming convention here to the naming convention of the table in the json object
	/// </summary>
	[Key]
 	[Column(HelperVariables.ItemId)]
 	public string ItemId { get; set; } = "";
 	public string ItemDescription { get; set; } = "";
}