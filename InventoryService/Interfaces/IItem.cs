namespace InventoryService.Interfaces;

public interface IItem
{
	public string ItemId { get; set; }
	public string ItemDescription { get; set; }
	public string ItemType { get; set; }
}