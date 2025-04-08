using System;
using UnityEngine;

namespace ServerItems
{
	[Serializable]
	public class FoodItem : ItemBase
	{
		[field:SerializeField] public int Calories { get; set; }
		[field:SerializeField] public string ExpirationDate { get; set; } = "";
		[field:SerializeField] public bool IsConsumable { get; set; }

		public override string SerializeItem()
		{
			CurrentJsonType = $"{nameof(FoodItem)}";
			return base.SerializeItem();
		}

		public override string ToString()
		{
			return $"{ItemName}\n({(IsConsumable ? "": "Not")}Consumable)\n" +
			       $"Calories: {Calories}\n" +
			       $"Expires: {ExpirationDate}";
		}
	}
}