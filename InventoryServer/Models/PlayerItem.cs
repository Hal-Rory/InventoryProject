using System.ComponentModel.DataAnnotations.Schema;
using InventoryProject.Helpers;
using Microsoft.EntityFrameworkCore;

namespace InventoryProject.Models;
[Keyless]
public class PlayerItem
{
	/// <summary>
	/// This converts the naming convention here to the naming convention of the table in the json object
	/// </summary>
	[Column(HelperVariables.ItemId)]
	public string ItemId { get; set; } = "";
	public string ItemDescription { get; set; } = "";
	public int ItemQuantity { get; set; } = 0;
}