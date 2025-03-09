using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryService.Helpers;
namespace InventoryService.Models;
public class ItemBase
{
	[Key]
 	[Column(HelperVariables.ItemId)]
 	public virtual string ItemId { get; set; }
 	public string ItemDescription { get; set; }
 	public string ItemType { get; set; }
}