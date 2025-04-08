using System;
using UnityEngine;

namespace ServerItems
{
	[Serializable]
	public class WeaponItem : ItemBase
	{
		[field: SerializeField] public int Damage { get; set; }
		[field: SerializeField] public int Durability { get; set; }
		[field: SerializeField] public bool IsRanged { get; set; }
		public override string SerializeItem()
		{
			CurrentJsonType = $"{nameof(WeaponItem)}";
			return base.SerializeItem();
		}

		public override string ToString()
		{
			return $"{ItemName}\n({(IsRanged ? "Ranged": "Melee")})\n" +
			       $"Damage: {Damage}\n" +
			       $"Durability: {Durability}";
		}
	}
}