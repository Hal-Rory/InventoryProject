namespace ServerItems
{
	public class FoodItem : ItemBase
	{
		public int Calories { get; set; }
		public string ExpirationDate { get; set; } = "";
		public bool IsConsumable { get; set; }

		public override string SerializeItem()
		{
			CurrentJsonType = $"{nameof(WeaponItem)}";
			return base.SerializeItem();
		}
	}
}