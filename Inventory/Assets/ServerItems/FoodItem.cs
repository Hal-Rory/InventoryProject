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

		public override string ToString()
		{
			return $"{ItemName}\n" +
			       $"Calories: {Calories}\n" +
			       $"Expires: {ExpirationDate}";
		}
	}
}