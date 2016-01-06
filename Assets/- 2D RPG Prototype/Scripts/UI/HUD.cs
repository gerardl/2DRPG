using UnityEngine;
using UnityEngine.UI;

namespace RPGPrototype.UI
{
    public class HUD : UIWindow
    {
        public Slider hpBar;
        public Slider mpBar;
        public Text currentHealth;
        public Text maxHealth;
        public Text currentMana;
        public Text maxMana;
        public ItemSlot[] usableSlots;
        public ItemSlot weaponSlot;
        public Text errorText;

        float initialHpWidth;
        float initialMpWidth;

        void Start()
        {
            hpBar.minValue = 0;
            mpBar.minValue = 0;
        }

        void Update()
        {
            var player = Player.PlayerController.Instance;

            // HP
            var maxHp = player.MaxHealth;
            var hp = player.Health;

            currentHealth.text = hp.ToString();
            maxHealth.text = maxHp.ToString();
            hpBar.maxValue = maxHp;
            hpBar.value = hp;

            // MP
            var maxMp = player.MaxMana;
            var mp = player.Mana;

            currentMana.text = mp.ToString();
            maxMana.text = maxMp.ToString();
            mpBar.maxValue = maxMp;
            mpBar.value = mp;

            //float hpPercent = (player.Health / player.MaxHealth) * 100;
            //Debug.Log(hpPercent);
            //hpBar.sizeDelta = new Vector2(hpBar.rect.height, hpPercent);

            weaponSlot.SetItem(player.inventoryController.Weapon);
            foreach (ItemSlot slot in usableSlots)
            {
                slot.SetItem(Player.InventoryController.Instance.Items[slot.id]);
            }
            
        }
    }
}
