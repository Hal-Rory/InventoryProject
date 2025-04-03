using System;
using Newtonsoft.Json;
using Server;
using UnityEngine;

namespace ServerItems
{
	[Serializable]
	public class ItemBase : IItem
	{
		public string CurrentJsonSchema { get; set; }
		public string CurrentJsonType { get; set; }
		[field: SerializeField] public string ItemName { get; set; } = "";
		[field: SerializeField] public string ItemID { get; set; } = "";

		[field: SerializeField] public string[] Ingredients { get; set; }

		public virtual string SerializeItem()
		{
			CurrentJsonSchema = ServerUtilities.CurrentJsonSchema;
			string json = JsonConvert.SerializeObject(this, ServerUtilities.JsonSerializer);
			return json;
		}

		public static ItemBase DeserializeItem(string item)
		{
			return JsonConvert.DeserializeObject<ItemBase>(item, ServerUtilities.JsonSerializer );
		}

		public bool Matches(ItemBase item)
		{
			bool isEqual = item.ItemID == ItemID && item.ItemName == ItemName && item.Ingredients == Ingredients;
			return isEqual;
		}
	}
}