using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryService.Helpers;
using InventoryService.Interfaces;
namespace InventoryService.Models;
public class ItemBase : IItem
{
	[Key]
 	[Column(HelperVariables.ItemId)]
 	public string ItemId { get; set; }
 	public string ItemDescription { get; set; }
 	public string ItemType { get; set; }
}