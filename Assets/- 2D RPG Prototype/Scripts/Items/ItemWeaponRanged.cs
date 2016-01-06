using UnityEngine;

namespace RPGPrototype.Items
{
    [System.Serializable]
    public class ItemWeaponRanged : ItemWeapon
    {
        public GameObject projectile;

        public ItemWeaponRanged()
            : base()
        {
            itemType = ItemTypes.RangedWeapon;
        }
    }
}
