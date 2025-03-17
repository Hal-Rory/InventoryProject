namespace ServerItems
{
	public class WeaponItem : ItemBase
	{
		public int Damage { get; set; }
		public int Durability { get; set; }
		public bool IsRanged { get; set; }
		public override string SerializeItem()
		{
			CurrentJsonType = $"{nameof(WeaponItem)}";
			return base.SerializeItem();
		}
	}
}