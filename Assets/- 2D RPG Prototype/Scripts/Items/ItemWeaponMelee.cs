using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.Items
{
    [System.Serializable]
    public class ItemWeaponMelee : ItemWeapon
    {
        public enum MeleeWeaponTypes
        {
            Sword = 0,
            Axe = 1,
            Dagger = 2,
            GreatSword = 3,
            GreatAxe = 4
        }

        public Combat.DamageInfo damage;
        public float attackArc = .5f;
        public float attackSpeed = .5f;
        public MeleeWeaponTypes meleeWeaponType;

        public ItemWeaponMelee()
            : base()
        {
            itemType = ItemTypes.MeleeWeapon;
            meleeWeaponType = MeleeWeaponTypes.Sword;
            damage = new Combat.DamageInfo();
        }
    }
}
