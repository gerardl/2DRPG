using System;
using UnityEngine;

namespace RPGPrototype.Interactive
{
    public class Drop : MonoBehaviour, IInteractive
    {
        public string itemId = "";
        public SpriteRenderer spriteRenderer;
        private Items.Item item;

        public string Name { get { return item.Name; } }
        public bool IsActive { get { return true; } }
        public InteractiveAction Action { get { return InteractiveAction.Take; } }

        void Start()
        {
            item = Items.ItemDatabase.Instance.GetItemById(itemId);
            if (item == null)
            {
                Destroy(gameObject);
                return;
            }
            spriteRenderer.sprite = item.Icon;
        }

        public void Use()
        {
            if (Player.PlayerController.Instance.inventoryController.Additem(item))
            {
                // play a pickup sound
                Destroy(gameObject);
            }
            else
            {
                UI.UIController.Instance.systemMessageScreen.ShowSystemMessage("Inventory Is full", null);
            }
        }
    }
}
