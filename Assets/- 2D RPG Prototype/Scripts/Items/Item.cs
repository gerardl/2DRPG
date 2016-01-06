using UnityEngine;
using System.Collections;

namespace RPGPrototype.Items
{
    [System.Serializable]
    public abstract class Item
    {
        public enum ItemTypes
        {
            Quest = 0,
            Consumable = 1,
            MeleeWeapon = 2,
            RangedWeapon = 3,
            MagicWeapon = 4,
            Wearable = 5
        }

        public string Id;
        public string Name;
        public string Description;
        public Sprite Icon;
        public ItemTypes itemType;

        public virtual void Activate() { }
        public virtual void Deactivate() { }

        public Item() { }

        public override string ToString()
        {
            return Name;
        }
    }

}
