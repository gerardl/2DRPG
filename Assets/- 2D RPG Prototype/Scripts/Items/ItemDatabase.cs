using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RPGPrototype.Items
{
    // I'm going to start by making this an editable list in 
    // the unity client.  I will probably move this to a json
    // file when I try to implement local multiplayer.

    [System.Serializable]
    public class ItemDatabase : Helpers.Singleton<ItemDatabase>
    {
        public List<ItemQuest> questItems = new List<ItemQuest>();
        public List<ItemWeaponMelee> meleeWeapons = new List<ItemWeaponMelee>();
        public List<ItemWeaponRanged> rangedWeapons = new List<ItemWeaponRanged>();

        private List<Item> itemList = new List<Item>();

        public List<Item> Items
        {
            get
            {
                // how often to cache this?
                if (itemList.Count == 0)
                {
                    itemList.AddRange(questItems.ToArray());
                    itemList.AddRange(meleeWeapons.ToArray());
                    itemList.AddRange(rangedWeapons.ToArray());
                }
                return itemList;
            }
        }

        public Item GetItemById(string id)
        {
            if (string.IsNullOrEmpty(id))
                return null;

            return Items.Find(i => i.Id == id);
        }

        // For editor

        public void AddItem(Item.ItemTypes type)
        {
            switch (type)
            {
                case Item.ItemTypes.Quest:
                    questItems.Add(new ItemQuest());
                    break;
                case Item.ItemTypes.Consumable:
                    break;
                case Item.ItemTypes.MeleeWeapon:
                    meleeWeapons.Add(new ItemWeaponMelee());
                    break;
                case Item.ItemTypes.RangedWeapon:
                    rangedWeapons.Add(new ItemWeaponRanged());
                    break;
                case Item.ItemTypes.MagicWeapon:
                    break;
                case Item.ItemTypes.Wearable:
                    break;
                default:
                    break;
            }
        }

        public void RemoveItem(Item item)
        {
            switch (item.itemType)
            {
                case Item.ItemTypes.Quest:
                    questItems.Remove((ItemQuest)item);
                    break;
                case Item.ItemTypes.Consumable:
                    break;
                case Item.ItemTypes.MeleeWeapon:
                    meleeWeapons.Remove((ItemWeaponMelee)item);
                    break;
                case Item.ItemTypes.RangedWeapon:
                    rangedWeapons.Remove((ItemWeaponRanged)item);
                    break;
                case Item.ItemTypes.MagicWeapon:
                    break;
                case Item.ItemTypes.Wearable:
                    break;
                default:
                    break;
            }
        }
    }
}
