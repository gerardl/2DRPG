using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RPGPrototype.UI
{
    public class ItemSlot : MonoBehaviour
    {
        public enum SlotTypes
        {
            Backpack = 0,
            Equipment = 1,
            Drop = 2
        }

        public Image icon;
        public Text numText;
        public SlotTypes slotType = SlotTypes.Backpack;
        public Items.Item SlottedItem { get; private set; }
        public int id = 0;
        public bool ShowSlotNumber = false;

        void Start()
        {
            if (ShowSlotNumber == true)
            {
                numText.gameObject.SetActive(true);
                numText.text = (id + 1).ToString();
            }
            else
            {
                numText.gameObject.SetActive(false);
            }
        }

        public void SetItem(Items.Item item)
        {
            SlottedItem = item;
            if (item == null || item.Id == "")
            {
                icon.gameObject.SetActive(false);
            }
            else
            {
                icon.gameObject.SetActive(true);
                icon.sprite = item.Icon;
            }
        }
    }
}
