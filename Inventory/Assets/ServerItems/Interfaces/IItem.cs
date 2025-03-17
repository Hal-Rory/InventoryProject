public interface IItem
{
	public string CurrentJsonSchema { get ; set;}
	public string CurrentJsonType { get ; set;}
	public string ItemName { get; set; }
	public string ItemID { get; set; }
	public string SerializeItem();
}