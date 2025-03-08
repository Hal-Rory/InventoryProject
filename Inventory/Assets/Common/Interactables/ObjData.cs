using GameInventory.ScriptableObjectData;
using UnityEngine;

namespace Common.Interactables
{
    public enum RANK_NAME { Bronze, Silver, Gold, Platinum };
    public class ObjData : ScriptableObject
    {
        public string DisplayName;
        [TextArea]
        public string Description;
        public Sprite DisplaySprite;
        public string ID;
        public RANK_NAME Rank = 0;

        public virtual void Copy(ItemData other)
        {
            DisplayName = other.DisplayName;
            Description = other.Description;
            DisplaySprite = other.DisplaySprite;
            ID = other.ID;
            Rank = other.Rank;
        }
    }
}