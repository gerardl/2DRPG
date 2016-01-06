using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.Items
{
    public class ItemPotion : Item
    {
        public enum PotionTypes
        {
            Health = 0,
            Mana = 1
        }

        public PotionTypes potionType;
        public int healthToAdd;
        public int manaToAdd;

        public ItemPotion()
        {
            itemType = ItemTypes.Consumable;
        }

        public override void Activate()
        {
            base.Activate();

        }
    }
}
