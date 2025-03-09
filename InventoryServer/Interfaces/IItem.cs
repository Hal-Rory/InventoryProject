namespace InventoryProject.Interfaces;

public interface IItem
{
	public string ItemName { get; set; }
	public string ItemID { get; set; }
	public string SerializeItem();
}