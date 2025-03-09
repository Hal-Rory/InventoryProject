﻿using System.Text.Json.Serialization;
using InventoryService.Helpers;
using InventoryService.Interfaces;
using Newtonsoft.Json;

namespace InventoryService.Models;

public class FoodItem : IItem
{
	[JsonPropertyName(HelperVariables.JsonSchema)]
	public string Schema => HelperVariables.CurrentJsonSchema;
	public string ItemName { get; set; }
	public string ItemID { get; set; }

	public int Calories { get; set; }
	public string ExpirationDate { get; set; }
	public bool IsConsumable { get; set; }

	public string SerializeItem()
	{
		string json = JsonConvert.SerializeObject(this);
		return json;
	}
}