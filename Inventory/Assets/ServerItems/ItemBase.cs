using Newtonsoft.Json;
using Server;

namespace ServerItems
{
	public class ItemBase : IItem
	{
		public string CurrentJsonSchema { get; set; }
		public string CurrentJsonType { get; set; }

		public string ItemName { get; set; } = "";
		public string ItemID { get; set; } = "";

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
	}
}