using Newtonsoft.Json;

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
}