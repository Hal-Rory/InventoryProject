using System.Text.Json.Serialization;
using InventoryService.Helpers;
using InventoryService.Interfaces;
using Newtonsoft.Json;

namespace InventoryService.Models;

public class WeaponItem : IItem
{
	[JsonPropertyName(HelperVariables.JsonSchema)]
	public string Schema => HelperVariables.CurrentJsonSchema;

	public string ItemName { get; set; }
	public string ItemID { get; set; }
	public int Damage { get; set; }
	public int Durability { get; set; }
	public bool IsRanged { get; set; }
	public string SerializeItem()
	{
		string json = JsonConvert.SerializeObject(this);
		return json;
	}
}