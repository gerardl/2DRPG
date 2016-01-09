using System.Collections.Generic;
using UnityEngine;
using RPGPrototype.Items;
using RPGPrototype.Management;

namespace RPGPrototype.Player
{
    public class InventoryController : Helpers.Singleton<InventoryController>
    {
        public event System.Action<ItemWeapon> onSetWeapon;
        //public event System.Action<ItemWearable> onSetWearable;

        // total number of items you can carry
        // at one time
        public int maxItemCount = 24;
        private List<Item> items;

        // All Items
        public List<Item> Items { get { return items; } }
        // Currently Equipped Weapon
        public ItemWeapon Weapon { get; private set; }

        void Awake()
        {
            items = new List<Item>(new Item[maxItemCount]);
        }

        void Update()
        {
            if (!PlayerController.Instance.weaponController.IsLastAttackComplete) return;

            if (InputController.Item1) UseItem(0);
            if (InputController.Item2) UseItem(1);
            if (InputController.Item3) UseItem(2);
            if (InputController.Item4) UseItem(3);
            if (InputController.Item5) UseItem(4);
            if (InputController.Item6) UseItem(5);
            if (InputController.Item7) UseItem(6);
            if (InputController.Item8) UseItem(7);
            if (InputController.Item9) UseItem(8);
            if (InputController.Item0) UseItem(9);
        }

        public void SetActiveWeapon(ItemWeapon weapon)
        {
            // deactivate old weapon
            if (Weapon != null) Weapon.Deactivate();
            // equip new weapon
            Weapon = weapon;
            if (onSetWeapon != null) onSetWeapon(weapon);

            UI.UIController.Instance.systemMessageScreen.ShowSystemMessage("You equipped " + weapon.Name, weapon.Icon);
        }

        public Item GetItemById(string itemId)
        {
            if (string.IsNullOrEmpty(itemId)) return null;
            return Items.Find(i => i.Id == itemId);
            
        }

        public void UseItem(int slotId)
        {
            // Invalid Slot
            if (slotId < 0 || slotId > items.Count - 1)
                return;

            Item item = items[slotId];
            
            // Couldn't find the item, or it was a quest item -- can't use
            if (item == null || item.itemType == Item.ItemTypes.Quest)
                return;

            items[slotId] = null;
            item.Activate();
        }

        public bool Additem(Item item)
        {
            var emptyIndex = items.FindIndex(i => i == null);
            // no open slot
            if (emptyIndex == -1)
                return false;

            items[emptyIndex] = item;
            UI.UIController.Instance.systemMessageScreen.ShowSystemMessage("You picked up " + item.Name, item.Icon);
            return true;
        }
    }
}
